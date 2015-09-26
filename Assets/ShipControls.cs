using UnityEditor;
using UnityEngine;

namespace Assets
{
	public class ShipControls : MonoBehaviour
	{
		private new Rigidbody2D rigidbody;

		private Vector3 goal;
		public float turnSpeed = 10f;
		public float drag = .99f;
		public float rotSpeed = 180f;
		public Vector3 velocity;
		public float acceleration = 10f;
		public float maxSpeed = 15f;
		public Ship ship;
		public Transform nose;

		public float goalDistance = 5f;
		public float dragScale = 0.5f;
		public string horizontalAxisName = "Player1Horizontal";
		public string verticalAxisName = "Player1Vertical";
		public bool disabled = false;

		public void Start()
		{
			rigidbody = GetComponent<Rigidbody2D>();
		}
		public void Update()
		{
			HandleKeys();
		}

		private void HandleKeys()
		{
			if (disabled)
				return;

			// Create a goal in space
			var goal = nose.position + new Vector3(Input.GetAxis(horizontalAxisName), Input.GetAxis(verticalAxisName)) * goalDistance;
			Debug.DrawLine(nose.position, goal, Color.green);

			// Drag the ship by the nose to the goal
			rigidbody.AddForceAtPosition((goal - nose.position) * dragScale, nose.position);
		}

		private void HandleKeys2()
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
			var thrust = Mathf.Max(Input.GetAxis("Vertical"), 0f);
			velocity += transform.up * thrust * acceleration * Time.deltaTime;

			// Limit the ship's top speed
			velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

			// Apply drag
			velocity += -velocity.normalized * drag * velocity.magnitude;

			// Update the position
			pos += velocity * Time.deltaTime;
			transform.position = pos;

			// Update how much the ship's engine is thrusting
			ship.engineThrust = thrust;
		}
	}
}
