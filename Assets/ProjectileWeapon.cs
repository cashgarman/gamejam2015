using System.Collections;
using UnityEngine;

namespace Assets
{
	public class ProjectileWeapon : Weapon
	{
		public GameObject projectilePrefab;
		public float reloadRate = 0.5f;
		public float delay;
		private bool firing;
		private float timeSinceLastFire;
		protected float cooldown;
		public string fireSoundName;
		public float fireSoundVolume = 1f;
		public float minSpin;
		public float maxSpin;
		public float colliderEnableDelay;
		public float minSpreadForce;
		public float maxSpreadForce;

		public override void StartFire()
		{
			base.StartFire();

			if(cooldown <= 0)
				FireProjectile();

			firing = true;
		}

		public override void EndFire()
		{
			base.EndFire();

			firing = false;
		}

		public void Update()
		{
			cooldown -= Time.deltaTime;

			if (firing && cooldown <= 0)
				FireProjectile();
		}

		protected void FireProjectile()
		{
			// Play the fire sound
			Sounds.PlayOneShot(fireSoundName, fireSoundVolume);

			cooldown = reloadRate;

			if (delay > 0)
			{
				StartCoroutine(DelayFireProjectile());
				return;
			}
			
			FireProjectileInternal();
		}

		protected virtual void FireProjectileInternal()
		{
			// Spawn a projectile
			var projectileObject = Instantiate(projectilePrefab, ship.muzzle.position, ship.muzzle.rotation) as GameObject;
			var projectile = projectileObject.GetComponent<Projectile>();
			projectile.owner = ship;
			projectile.firingWeapon = this;

			projectile.GetComponent<Rigidbody2D>().AddTorque(Random.Range(minSpin, maxSpin));
			projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.Cross(ship.transform.up, new Vector3(0, 0, 1)) *
				Random.Range(minSpreadForce, maxSpreadForce) / Time.deltaTime);

			if (colliderEnableDelay > 0)
			{
				projectile.GetComponent<Collider2D>().enabled = false;
				StartCoroutine(DelayEnableCollision(projectile));
			}
		}

		private IEnumerator DelayEnableCollision(Projectile projectile)
		{
			yield return new WaitForSeconds(colliderEnableDelay);
			projectile.GetComponent<Collider2D>().enabled = true;
		}

		protected IEnumerator DelayFireProjectile()
		{
			yield return new WaitForSeconds(delay);
			FireProjectileInternal();
		}
	}
}