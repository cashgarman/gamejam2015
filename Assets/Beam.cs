using System.Linq;
using UnityEngine;

namespace Assets
{
	public class Beam : MonoBehaviour
	{
		public float beamLength;
		public Weapon weapon;
		public float damagePerSecond;

		public void Update()
		{
			var hits = Physics2D.RaycastAll(transform.position, transform.up, beamLength);
			var hit = hits.FirstOrDefault(h => h.collider != null && h.collider.GetComponent<SpaceObject>() != null &&
												h.collider.GetComponent<SpaceObject>() != weapon.ship);
			if (hit.collider != null)
			{
				var spaceObject = hit.collider.GetComponent<SpaceObject>();
				if (spaceObject != null)
				{
					if (spaceObject != weapon.ship)
						// Do damage to the thing
						spaceObject.TakeDamage(damagePerSecond * Time.deltaTime);
				}
			}
		}
	}
}