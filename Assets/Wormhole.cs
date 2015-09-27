using UnityEngine;

namespace Assets
{
	public class Wormhole : MonoBehaviour
	{
		public Wormhole otherSide;

		public void OnCollisionEnter2D(Collision2D collision)
		{
			OnHit(collision.collider);
		}

		public void OnTriggerEnter2D(Collider2D collider)
		{
			OnHit(collider);
		}	

		private void OnHit(Collider2D collider)
		{
			var spaceObject = collider.GetComponent<SpaceObject>();
			if (spaceObject == null)
				return;

			if (spaceObject.immuneToWormholes)
				return;

			// Teleport the collider to the other side of wormhole
			collider.transform.position = otherSide.transform.position;

			// Make the thing immune to the wormhole for a moment
			spaceObject.wormholeImmuneTime = GameSettings.instance.wormholeImmunityTime;
			spaceObject.immuneToWormholes = true;
		}

		public void Update()
		{
			Debug.DrawLine(transform.position, otherSide.transform.position, Color.blue);
		}
	}
}
