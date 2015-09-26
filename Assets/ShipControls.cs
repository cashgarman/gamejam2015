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

			// Turn towards the goal
			rigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * GameSettings.instance.movementSpeed);
		}
	}
}
