using System.Collections;
using UnityEngine;

namespace Assets
{
	public class BeamWeapon : Weapon
	{
		public GameObject beamPrefab;
		private Beam beam;
		public string fireSoundName;
		public float delay;
		private bool firing;
		public float energyUsage;

		public override void StartFire()
		{
			if (beam != null)
				return;

			firing = true;

			// Play the fire sound
			Sounds.PlayOneShot(fireSoundName);

			if (delay > 0)
			{
				StartCoroutine(DelayFireBeam());
				return;
			}

			FireBeamInternal();
		}

		private void FireBeamInternal()
		{
			if (!firing)
				return;

			// Create the beam
			SpawnBeam();
		}

		private IEnumerator DelayFireBeam()
		{
			yield return new WaitForSeconds(delay);
			FireBeamInternal();
		}

		private void SpawnBeam()
		{
			// Create the beam
			var beamObject = Instantiate(beamPrefab, ship.muzzle.position, ship.transform.rotation) as GameObject;
			beamObject.transform.parent = ship.transform;
			beam = beamObject.GetComponent<Beam>();
			beam.weapon = this;
		}

		public override void EndFire()
		{
			if (beam == null)
				return;

			firing = false;

			// Destroy the beam
			Destroy(beam.gameObject);
		}

		public void Update()
		{
			if (beam != null)
				ship.energyUsage += energyUsage;
		}
	}
}
