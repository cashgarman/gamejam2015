using UnityEngine;

namespace Assets
{
	public class Projectile : SpaceObject
	{
		public float acceleratingForce = 10f;
		public Ship owner;
		public bool doesDamage = true;
		public bool friendlyFire = true;
		public float damage = 10f;
		private float age;
		public float friendlyFireImmunityTime = 0.25f;
		public bool destroyOnHit = true;
		public new ParticleSystem particleSystem;
		public float particleSystemLife = 5f;

		public void Start()
		{
			// Accelerate forward
			var rigidbody = GetComponent<Rigidbody2D>();
			rigidbody.velocity = owner.GetComponent<Ship>().velocity;
			rigidbody.AddForce(transform.up * acceleratingForce / Time.deltaTime);
		}

		public void Update()
		{
			base.Update();

			age += Time.deltaTime;
		}

		public float GetDamage(Ship target)
		{
			if (target == owner && age < friendlyFireImmunityTime)
				return 0;

			return damage;
		}

		public void OnHit(Ship target)
		{
			if (destroyOnHit)
				OnDestroy();
				Destroy(gameObject);
		}

		private void OnDestroy()
		{
			particleSystem.Stop();
			particleSystem.transform.parent = null;
			Destroy(particleSystem.gameObject, particleSystemLife);
		}

		public bool CanHit(Ship target)
		{
			if (target == owner && age < friendlyFireImmunityTime)
				return false;
			return true;
		}
	}
}
