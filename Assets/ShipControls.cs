using UnityEditor;
using UnityEngine;

namespace Assets
{
	public class ShipControls : MonoBehaviour
	{
		private float forwardThrust;
		private float turningThrust;

		private Vector3 goal;
		public float turnSpeed = 10f;
		public float drag = .99f;
		public float rotSpeed = 180f;
		public Vector3 velocity;
		public float acceleration = 10f;

		public void Update()
		{
			HandleKeys();
		}

		private void HandleKeys()
		{
			// ROTATE the ship.

			// Grab our rotation quaternion
			var rot = transform.rotation;

			// Grab the Z euler angle
			var z = rot.eulerAngles.z;

			// Change the Z angle based on input
			z -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

			// Recreate the quaternion
			rot = Quaternion.Euler(0, 0, z);

			// Feed the quaternion into our rotation
			transform.rotation = rot;

			var pos = transform.position;

			// Update the velocity
			velocity += transform.up * Mathf.Max(Input.GetAxis("Vertical"), 0f) * acceleration * Time.deltaTime;

			// Apply drag
			velocity += -velocity.normalized * drag * velocity.magnitude;

			// Update the position
			pos += velocity * Time.deltaTime;
			transform.position = pos;
		}
	}
}
