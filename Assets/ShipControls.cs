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
		private float rotSpeed = 180f;
		private float maxSpeed = 5f;

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
//			forwardThrust = Input.GetAxis("Vertical");
//			turningThrust = Input.GetAxis("Horizontal");
//
//			goal = transform.position + new Vector3(turningThrust, 0f, forwardThrust);
//			Debug.DrawLine(transform.position, goal, Color.green);
//
//			// Calculate the angle between the player and the movement goal
////			var angleDelta = Quaternion.FromToRotation(transform.up, goal - transform.position);
////			transform.Rotate(angleDelta.eulerAngles, Time.deltaTime * turnSpeed);
//			transform.Rotate(new Vector3(0f, 0f, 1f), Time.deltaTime * -turningThrust * turnSpeed);
//
//			// Drag the player towards the goal
//			rigidbody.AddForce((goal - transform.position).normalized * (GameSettings.instance.movementSpeed * ((1f + Input.GetAxis("RightTrigger")) * GameSettings.instance.boostFactor)));
//
//			// Rotate the player towards the direction they're heading
//			transform.LookAt(goal);
//
//			// Apply drag
//			rigidbody.AddForce(rigidbody.velocity * -drag);

			// ROTATE the ship.

			// Grab our rotation quaternion
			Quaternion rot = transform.rotation;

			// Grab the Z euler angle
			float z = rot.eulerAngles.z;

			// Change the Z angle based on input
			z -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

			// Recreate the quaternion
			rot = Quaternion.Euler(0, 0, z);

			// Feed the quaternion into our rotation
			transform.rotation = rot;

			// MOVE the ship.
			Vector3 pos = transform.position;

			Vector3 velocity = new Vector3(0, Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime, 0);

			pos += rot * velocity;

			// RESTRICT the player to the camera's boundaries!

			// First to vertical, because it's simpler
//			if (pos.y + shipBoundaryRadius > Camera.main.orthographicSize)
//			{
//				pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
//			}
//			if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize)
//			{
//				pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
//			}

			// Now calculate the orthographic width based on the screen ratio
			float screenRatio = (float)Screen.width / (float)Screen.height;
			float widthOrtho = Camera.main.orthographicSize * screenRatio;

			// Now do horizontal bounds
//			if (pos.x + shipBoundaryRadius > widthOrtho)
//			{
//				pos.x = widthOrtho - shipBoundaryRadius;
//			}
//			if (pos.x - shipBoundaryRadius < -widthOrtho)
//			{
//				pos.x = -widthOrtho + shipBoundaryRadius;
//			}

			// Finally, update our position!!
			transform.position = pos;
		}
	}
}
