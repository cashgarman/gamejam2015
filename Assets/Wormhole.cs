using System.Collections;
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

			// Spawn a wormhole enter effect
			SpawnWormholeEnterEffect();

			// Play the wormhole enter sound
			var ship = collider.GetComponent<Ship>();
			if(ship != null)
				Sounds.PlayOneShot("WormholeShipEnter");
			else
				Sounds.PlayOneShot("WormholeObjectEnter");

			// Make the thing immune to the wormhole for a moment
			spaceObject.wormholeImmuneTime = GameSettings.instance.wormholeImmunityTime;
			spaceObject.immuneToWormholes = true;

			// If the entering object is a ship
			
			if (ship != null)
			{
				// Hide the player
				ship.SetVisible(false);

				// Kill the players
				ship.KillSpeed();

				// Start moving them to the other wormhole
				StartCoroutine(MoveShipToOtherSide(ship));
			}
			// If the entering object isn't a ship
			else
			{
				// Instantly teleport the collider to the other side of wormhole
				collider.transform.position = otherSide.transform.position;

				// Spawn the workhole exit effect
				otherSide.SpawnWormholeExitEffect();

				// Player the wormhole exit sound
				Sounds.PlayOneShot("WormholeObjectExit");
			}
		}

		private IEnumerator MoveShipToOtherSide(Ship ship)
		{
			while (Vector3.Distance(ship.transform.position, otherSide.transform.position) > GameSettings.instance.wormholeExitThreshold)
			{
				var direction = (otherSide.transform.position - ship.transform.position).normalized;

				var pos = ship.transform.position;
				pos += direction * GameSettings.instance.wormholeMovementSpeed * Time.deltaTime;
				ship.transform.position = pos;

				yield return null;
			}

			// Spawn the workhole exit effect
			otherSide.SpawnWormholeExitEffect();

			// Player the wormhole exit sound
			Sounds.PlayOneShot("WormholeShipExit");

			// Exit the wormhole
			ship.transform.position = otherSide.transform.position;
			ship.SetVisible(true);

			// Give the ship a random weapon
			ship.RandomizeWeapon();

			// Throw the player out of the wormhole
			ship.ApplyForce((otherSide.transform.position - transform.position).normalized *
							GameSettings.instance.wormholeExitImpulse / Time.deltaTime);
		}

		public void Update()
		{
			Debug.DrawLine(transform.position, otherSide.transform.position, Color.blue);
		}

		protected void SpawnWormholeEnterEffect()
		{
			// Create the effect
			var effectObject = Instantiate(GameSettings.instance.wormholeEnterEffectPrefab, transform.position, Util.RandomRotation()) as GameObject;

			// Destroy the effect after the duration
			Destroy(effectObject, GameSettings.instance.wormholeEffectDurationPrefab);
		}

		protected void SpawnWormholeExitEffect()
		{
			// Create the effect
			var effectObject = Instantiate(GameSettings.instance.wormholeExitEffectPrefab, transform.position, Util.RandomRotation()) as GameObject;

			// Destroy the effect after the duration
			Destroy(effectObject, GameSettings.instance.wormholeEffectDurationPrefab);
		}
	}
}
