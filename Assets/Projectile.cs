using System.Net.Cache;
using UnityEngine;

namespace Assets
{
	public class Projectile : MonoBehaviour
	{
		public float acceleratingForce = 10f;
		public Ship owner;
		public bool doesDamage = true;
		public bool friendlyFire = true;
		public float damage = 10f;
		private float age;
		public float friendlyFireImmunityTime = 0.25f;

		public void Start()
		{
			// Accelerate forward
			var rigidbody = GetComponent<Rigidbody2D>();
			rigidbody.velocity = owner.GetComponent<Ship>().velocity;
			rigidbody.AddForce(transform.up * acceleratingForce / Time.deltaTime);
		}

		public void Update()
		{
			age += Time.deltaTime;
		}

		public float GetDamage(Ship target)
		{
			if (age < friendlyFireImmunityTime)
				return 0;

			return damage;
		}
	}
}
