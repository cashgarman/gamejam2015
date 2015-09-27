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

		public void Fire()
		{
			// Spawn a bullet
			var bulletObject = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as GameObject;
			var bullet = bulletObject.GetComponent<Projectile>();
			bullet.owner = this;
		}

		public void Update()
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

			// Disable the player controls
			controls.disabled = true;

			// Destroy the ship
			Destroy(gameObject);

			// Respawn the ship
			Game.instance.SpawnPlayer(playerNumber);

			// Let the game know a player was killed
			Game.instance.OnPlayerKilled(playerNumber);
		}
	}
}