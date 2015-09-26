using UnityEditor;
using UnityEngine;

namespace Assets
{
	public class ShipControls : MonoBehaviour
	{
		private float forwardThrust;
		private float turningThrust;

		private new Rigidbody2D rigidbody;
		private Vector3 goal;
		public float turnSpeed = 10f;
		public float drag = .99f;

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
			forwardThrust = Input.GetAxis("Vertical");
			turningThrust = Input.GetAxis("Horizontal");

			goal = transform.position + new Vector3(turningThrust, forwardThrust);
			Debug.DrawLine(transform.position, goal, Color.green);

			transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * -turningThrust * turnSpeed);

			// Drag the player towards the goal
			rigidbody.AddForce(transform.up * forwardThrust * (GameSettings.instance.movementSpeed * ((1f + Input.GetAxis("RightTrigger")) * GameSettings.instance.boostFactor)));

			// Apply drag
			rigidbody.AddForce(rigidbody.velocity * -drag);
		}
	}
}
