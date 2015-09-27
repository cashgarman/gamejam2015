using UnityEngine;

namespace Assets
{
	public class TrackingProjectileWeapon : ProjectileWeapon
	{
		public SpaceObject target;

		protected override void FireProjectile()
		{
			// Spawn a tracking projectile
			var projectileObject = Instantiate(projectilePrefab, ship.muzzle.position, ship.muzzle.rotation) as GameObject;
			var projectile = projectileObject.GetComponent<Projectile>();
			projectile.owner = ship;

			// Intially target the other player
			target = Game.GetOtherShip(ship);

			cooldown = reloadRate;
		}
	}
}
