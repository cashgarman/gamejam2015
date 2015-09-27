using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
	public class Ship : SpaceObject
	{
		public GameObject bulletPrefab;
		public Transform muzzle;
		public Vector2 velocity;
		private Vector3 prevPosition;
		public float startingFuel = 100f;
		public float fuelUseRate = 1f;
		public Text fuelText;
		private float fuel;
		public float engineThrust;

		public Text healthText;
		public int playerNumber = 1;

		public ShipControls controls;
		public ParticleSystem engineParticles;
		private Text speedText;
		public Weapon weapon;

		public new void Start()
		{
			base.Start();

			prevPosition = transform.position;
			fuel = startingFuel;

			if (playerNumber == 1)
			{
				Game.instance.player1Ship = this;
				Game.instance.followCamera.player1 = transform;
				healthText = Game.instance.player1HealthText;
				speedText = Game.instance.player1SpeedText;
			}
			else
			{
				Game.instance.player2Ship = this;
				Game.instance.followCamera.player2 = transform;
				healthText = Game.instance.player2HealthText;
				speedText = Game.instance.player2SpeedText;
			}

			UpdateDisplay();
		}

		public void StartFire()
		{
			if (controls.disabled)
				return;

			// Start firing the current weapon
			weapon.StartFire();
		}

		public void EndFire()
		{
			// Finish firing the current weapon
			weapon.EndFire();
		}

		public new void Update()
		{
			base.Update();

			velocity = (transform.position - prevPosition) / Time.deltaTime;
			prevPosition = transform.position;

			fuel -= engineThrust * fuelUseRate * Time.deltaTime;

			UpdateDisplay();
		}

		private void UpdateDisplay()
		{
			if (fuelText != null)
				fuelText.text = string.Format("Fuel: {0:F1}", fuel);

			if (healthText != null)
				healthText.text = string.Format("Player {0} Health: {1:F1}%", playerNumber, health);

			if (speedText != null)
				speedText.text = string.Format("Player {0} Speed: {1:F1}%", playerNumber, rigidbody.velocity.magnitude);
		}

		public override void OnDestroyed()
		{
			base.OnDestroyed();

			// Spawn an explosion
			SpawnExplosion(GameSettings.instance.shipExplosionSize, GameSettings.instance.explosionDuration);

			// Disable the player
			SetDisabled(true);

			// Destroy the ship
			Destroy(gameObject);

			// Respawn the ship
			Game.instance.SpawnPlayer(playerNumber);

			// Let the game know a player was killed
			Game.instance.OnPlayerKilled(playerNumber);
		}

		public void SetDisabled(bool disabled)
		{
			controls.disabled = disabled;
			if(disabled)
				weapon.EndFire();
		}

		public void InstallWeapon(GameObject weaponPrefab)
		{
			if(weapon != null)
				UninstallWeapon();

			Debug.Log("Installing weapon " + weaponPrefab.name);

			var weaponObject = Instantiate(weaponPrefab);
			weaponObject.transform.parent = transform;
			weapon = weaponObject.GetComponent<Weapon>();
			weapon.ship = this;
		}

		public void UninstallWeapon()
		{
			Debug.Log("Uninstalling weapon " + weapon.name);

			DestroyImmediate(weapon.gameObject);
			weapon = null;
		}

		public void SetVisible(bool visible)
		{
			Debug.Log("Setting visibility of " + name + " to " + visible);
			SetDisabled(!visible);
			gameObject.SetActive(visible);
			rigidbody.isKinematic = !visible;
		}

		public void RandomizeWeapon()
		{
			// Install a random weapon on the ship
			InstallWeapon(Util.RandomElement(GameSettings.instance.weapons));
		}
	}
}