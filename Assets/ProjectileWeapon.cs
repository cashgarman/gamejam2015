using UnityEngine;

namespace Assets
{
	public class ProjectileWeapon : Weapon
	{
		public GameObject projectilePrefab;
		public float reloadRate = 0.5f;
		private bool firing;
		private float timeSinceLastFire;
		private float cooldown;

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

		private void FireProjectile()
		{
			// Spawn a projectile
			var bulletObject = Instantiate(projectilePrefab, ship.muzzle.position, ship.muzzle.rotation) as GameObject;
			var bullet = bulletObject.GetComponent<Projectile>();
			bullet.owner = ship;

			cooldown = reloadRate;
		}
	}
}