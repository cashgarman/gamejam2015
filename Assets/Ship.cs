using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
	public class Ship : MonoBehaviour
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
		public float health;
		public float startingHealth;
		public int playerNumber = 1;

		public ShipControls controls;

		public void Start()
		{
			prevPosition = transform.position;
			fuel = startingFuel;
			health = startingHealth;

			UpdateFuelDisplay();

			if (playerNumber == 1)
			{
				Game.instance.player1Ship = this;
				Game.instance.followCamera.player1 = transform;
				healthText = Game.instance.player1HealthText;
			}
			else
			{
				Game.instance.player2Ship = this;
				Game.instance.followCamera.player2 = transform;
				healthText = Game.instance.player2HealthText;
			}
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
			velocity = (transform.position - prevPosition) / Time.deltaTime;
			prevPosition = transform.position;

			fuel -= engineThrust * fuelUseRate * Time.deltaTime;

			UpdateFuelDisplay();
			UpdateHealthDisplay();
		}

		private void UpdateFuelDisplay()
		{
			if (fuelText != null)
				fuelText.text = string.Format("Fuel: {0:F1}", fuel);
		}

		private void UpdateHealthDisplay()
		{
			if (healthText != null)
				healthText.text = string.Format("Player {0} Health: {1:F1}%", playerNumber, health);
		}

		public void OnCollisionEnter2D(Collision2D collision)
		{
			OnHit(collision.collider);
		}

		public void OnTriggerEnter2D(Collider2D collider)
		{
			OnHit(collider);
		}

		public void OnHit(Collider2D collider)
		{
			// If the collider is a projectile
			var projectile = collider.GetComponent<Projectile>();
			if (projectile != null)
			{
				// If the projectile should apply damage
				if (projectile.doesDamage && (this != projectile.owner || projectile.friendlyFire) && projectile.CanHit(this))
				{
					// Damage the ship
					var damage = projectile.GetDamage(this);
					Debug.Log("Received " + damage + " damage");
					health -= damage;

					// Check if we were destroyed
					if (health <= 0f)
						OnDestroyed();

					// Let the projectile know it hit something
					projectile.OnHit(this);
				}
			}

			Debug.Log("Hit by collider " + collider.name);
		}

		private void OnDestroyed()
		{
			// Spawn an explosion

			// Disable the player controls
			controls.disabled = true;

			// Destroy the ship
			Destroy(gameObject);

			// Respawn the ship
			Game.instance.SpawnPlayer(playerNumber);
		}
	}
}