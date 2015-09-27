﻿using UnityEngine;

namespace Assets
{
	public class CameraFollow : MonoBehaviour
	{
		public Transform player1;
		public Transform player2;
		public float distance;
		private new Camera camera;
		private float startingSize;
		public float maxCameraSize = 50f;

		public void Awake()
		{
			camera = GetComponent<Camera>();
			startingSize = camera.orthographicSize;
		}

		public void LateUpdate()
		{
			if (player1 == null || player2 == null)
				return;

			transform.position = ((player1.position + player2.position) / 2f) + Vector3.back * distance;

			var targetDistance = Vector3.Distance(player1.position, player2.position);
			camera.orthographicSize = Mathf.Clamp(targetDistance / 2f, startingSize, maxCameraSize);
		}
	}
}