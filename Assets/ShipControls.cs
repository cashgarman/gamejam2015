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
		public Ship ship;
		public Transform nose;

		public float goalDistance = 5f;
		public float dragScale = 0.5f;
		public string horizontalAxisName = "Player1Horizontal";
		public string verticalAxisName = "Player1Vertical";
		public string boostAxisName = "Player1Boost";
		public bool disabled = false;
		private float boost;

		public void Start()
		{
			rigidbody = GetComponent<Rigidbody2D>();
		}

		public void Update()
		{
			if (disabled)
				return;

			boost = Input.GetAxis(boostAxisName);

			// Create a goal in space
			var goal = nose.position + new Vector3(Input.GetAxis(horizontalAxisName), Input.GetAxis(verticalAxisName)) * goalDistance;
			Debug.DrawLine(nose.position, goal, Color.green);

			if (boost <= 0)
			{
				SetBoostEnabled(false);

				// Drag the ship by the nose to the goal at a normal speed
				rigidbody.AddForceAtPosition((goal - nose.position) * dragScale, nose.position);
			}
			else
			{
				SetBoostEnabled(true, boost);

				// Drag the ship by the nose to the goal at a boosted speed
				rigidbody.AddForceAtPosition((goal - nose.position) * dragScale * GameSettings.instance.boostFactor * boost, nose.position);
			}

			// Limit the ship speed
			rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, GameSettings.instance.maxShipSpeed);
		}

		private void SetBoostEnabled(bool enabled, float boost = 0f)
		{
			if (enabled)
			{
				ship.engineParticles.startSize = Mathf.Lerp(GameSettings.instance.normalEngineParticleSize, GameSettings.instance.boostEngineParticleSize, boost);
				ship.engineParticles.emissionRate = Mathf.Lerp(GameSettings.instance.normalEngineEmissionRate, GameSettings.instance.boostEngineEmissionRate, boost);
			}
			else
			{
				ship.engineParticles.startSize = GameSettings.instance.normalEngineParticleSize;
				ship.engineParticles.emissionRate = GameSettings.instance.normalEngineEmissionRate;
			}
		}
	}
}
