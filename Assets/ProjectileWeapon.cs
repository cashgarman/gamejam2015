using UnityEngine;

namespace Assets
{
	public class ProjectileWeapon : Weapon
	{
		public GameObject projectilePrefab;
		public float reloadRate = 0.5f;
		private bool firing;
		private float timeSinceLastFire;
		protected float cooldown;

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

		protected virtual void FireProjectile()
		{
			// Spawn a projectile
			var projectileObject = Instantiate(projectilePrefab, ship.muzzle.position, ship.muzzle.rotation) as GameObject;
			var projectile = projectileObject.GetComponent<Projectile>();
			projectile.owner = ship;
			projectile.firingWeapon = this;

			cooldown = reloadRate;
		}
	}
}