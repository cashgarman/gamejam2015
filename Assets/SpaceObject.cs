﻿using UnityEngine;

namespace Assets
{
	public class SpaceObject : MonoBehaviour
	{
		public new Rigidbody2D rigidbody;

		public float wormholeImmuneTime;
		public bool immuneToWormholes;

		public float health;
		public float startingHealth;
		public bool invinsible;

		public GameObject[] debrisChunkPrefabs;
		public float maxDebrisSpeed = 3f;
		public int numDebrisChunks = 3;

		public void Awake()
		{
			rigidbody = GetComponent<Rigidbody2D>();
		}

		public void Start()
		{
			health = startingHealth;
		}

		public void Update()
		{
			if (wormholeImmuneTime > 0)
			{
				wormholeImmuneTime -= Time.deltaTime;
				if (wormholeImmuneTime <= 0f)
					immuneToWormholes = false;
			}
		}

		public void OnCollisionEnter2D(Collision2D collision)
		{
			OnHit(collision.collider);
		}

		public void OnTriggerEnter2D(Collider2D collider)
		{
			OnHit(collider);
		}

		public virtual void OnHit(Collider2D collider)
		{
//			Debug.Log(name + " hit by collider " + collider.name);

			// If the collider is a projectile
			var projectile = collider.GetComponent<Projectile>();
			if (projectile != null)
			{
				// If the projectile should apply damage
				if (projectile.doesDamage && (this != projectile.owner || projectile.friendlyFire) && projectile.CanHit(this))
				{
					// Damage this object
					TakeDamage(projectile.GetDamage(this));

					// Let the projectile know it hit something
					projectile.OnHitSpaceObject(this);
				}
			}

			// If the collider is an asteroid
			var asteroid = collider.GetComponent<Asteroid>();
			if (asteroid != null)
			{
				// Damage this object
				TakeDamage(asteroid.GetImpactDamage(this));
			}
		}

		private void TakeDamage(float damage)
		{
//			Debug.Log("Received " + damage + " damage");
			health -= damage;

			// Check if we were destroyed
			if (health <= 0f && !invinsible)
				OnDestroyed();
		}

		public virtual void OnDestroyed()
		{
		}

		protected void SpawnExplosion(float scale, float duration)
		{
			// Create the explosion
			var explosionObject = Instantiate(GameSettings.instance.explosionPrefab, transform.position, Util.RandomRotation()) as GameObject;
			explosionObject.GetComponent<ParticleSystem>().startSize = scale;

			// Destroy the explosion after the duration
			Destroy(explosionObject, duration);
		}

		public void ApplyForce(Vector3 force)
		{
			rigidbody.AddForce(force);
		}

		public void KillSpeed()
		{
			rigidbody.velocity = Vector3.zero;
		}
	}
}