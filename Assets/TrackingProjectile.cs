using UnityEngine;

namespace Assets
{
	public class TrackingProjectile : Projectile
	{
		public float turningTorque = 0.1f;
		public SpaceObject target;
		public float accelerationForce = 1f;
		public float maxSpeed = 25f;
		public float minBeepInterval = 2f;
		public float maxBeepInterval = 0.1f;
		public bool shouldBeepOnProximity;
		public float minBeepDistance = 1f;
		public float maxBeepDistance = 50f;
		private float timeTillNextBeep;
		private float timeSinceLastBeep;
		public string beepSound;

		public new void Start()
		{
			base.Start();

			// Launch forward
			rigidbody.velocity = owner.GetComponent<Ship>().velocity;
			rigidbody.AddForce(transform.up * launchForce / Time.deltaTime);
		}

		// Credit: http://answers.unity3d.com/questions/161202/physics-based-homing-missile.html
		public new void Update()
		{
			base.Update();

			// If the target still exists (so it didn't die)
			if (target != null)
			{
				// Steer towards to the current target
				var targetDelta = target.transform.position - transform.position;

				// Get its cross product, which is the axis of rotation to get from one vector to the other
				var cross = Vector3.Cross(transform.up, targetDelta);

				// Normalise it and create a float, to hold a -1 or 1 corresponding to the right direction
				cross.Normalize();
				var turnDirection = cross.z;

				// Apply torque along that axis according to the magnitude of the angle.
				rigidbody.AddTorque(turningTorque * turnDirection);
			}

			// Accelerate forwards
			rigidbody.AddForce(transform.up * accelerationForce);
			rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);

			// Proximity beeping
			if (shouldBeepOnProximity)
			{
				var distance = Vector3.Distance(transform.position, target.transform.position);
				timeTillNextBeep = Mathf.Lerp(maxBeepInterval, minBeepInterval, (distance - minBeepDistance) / maxBeepDistance);

				timeSinceLastBeep += Time.deltaTime;
				if (timeSinceLastBeep >= timeTillNextBeep)
				{
					timeSinceLastBeep = 0f;
					Sounds.PlayOneShot(beepSound);
				}
			}
		}
	}
}
