using UnityEngine;

namespace Assets
{
	public class TrackingProjectileWeapon : ProjectileWeapon
	{
		protected override void FireProjectileInternal()
		{
			// Spawn a tracking projectile
			var projectileObject = Instantiate(projectilePrefab, ship.muzzle.position, ship.muzzle.rotation) as GameObject;
			var projectile = projectileObject.GetComponent<TrackingProjectile>();
			projectile.owner = ship;
			projectile.firingWeapon = this;

			projectile.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(minSpin, maxSpin);

			// Intially target the other player
			projectile.target = Game.GetOtherShip(ship);

			cooldown = reloadRate;
		}
	}
}
