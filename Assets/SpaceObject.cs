using UnityEngine;

namespace Assets
{
	public class SpaceObject : MonoBehaviour
	{
		public float wormholeImmuneTime;
		public bool immuneToWormholes;

		public float health;
		public float startingHealth;
		public bool invinsible;

		public GameObject[] debrisChunkPrefabs;
		public float maxDebrisSpeed = 3f;
		public int numDebrisChunks = 3;

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
			Debug.Log(name + " hit by collider " + collider.name);

			// If the collider is a projectile
			var projectile = collider.GetComponent<Projectile>();
			if (projectile != null)
			{
				// If the projectile should apply damage
				if (projectile.doesDamage && (this != projectile.owner || projectile.friendlyFire) && projectile.CanHit(this))
				{
					// Damage this object
					var damage = projectile.GetDamage(this);
					Debug.Log("Received " + damage + " damage");
					health -= damage;

					// Check if we were destroyed
					if (health <= 0f && !invinsible)
						OnDestroyed();

					// Let the projectile know it hit something
					projectile.OnHitSpaceObject(this);
				}
			}
		}

		public virtual void OnDestroyed()
		{
		}
	}
}