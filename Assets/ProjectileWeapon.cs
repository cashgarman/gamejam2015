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
			Sounds.PlayOneShot(fireSoundName);

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
		}

		protected IEnumerator DelayFireProjectile()
		{
			yield return new WaitForSeconds(delay);
			FireProjectileInternal();
		}
	}
}