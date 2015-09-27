using UnityEngine;

namespace Assets
{
	public class Projectile : SpaceObject
	{
		public float launchForce = 10f;
		public Ship owner;
		public ProjectileWeapon firingWeapon;
		public bool doesDamage = true;
		public bool friendlyFire = true;
		public float damage = 10f;
		private float age;
		public float friendlyFireImmunityTime = 0.5f;
		public bool destroyOnHit = true;
		public new ParticleSystem particleSystem;
		public float particleSystemLife = 5f;

		public new void Start()
		{
			base.Start();

			// Launch forward
			rigidbody.velocity = owner.GetComponent<Ship>().velocity;
			rigidbody.AddForce(transform.up * launchForce / Time.deltaTime);
		}

		public new void Update()
		{
			base.Update();

			age += Time.deltaTime;
		}

		public float GetDamage(SpaceObject target)
		{
			if (target == owner && age < friendlyFireImmunityTime)
				return 0;

			return damage;
		}

		public void OnHitSpaceObject(SpaceObject target)
		{
			if (destroyOnHit)
				OnDestroy();
		}

		private void OnDestroy()
		{
			if (particleSystem != null)
			{
				particleSystem.Stop();
				particleSystem.transform.parent = null;
				Destroy(particleSystem.gameObject, particleSystemLife);
			}
			Destroy(gameObject);
			OnDestroyed();
		}

		public override void OnHit(Collider2D collider)
		{
			// Projectile's don't handle their own collision, the things they hit do
		}

		public bool CanHit(SpaceObject target)
		{
			if (target == owner && age < friendlyFireImmunityTime)
				return false;
			return true;
		}
	}
}
