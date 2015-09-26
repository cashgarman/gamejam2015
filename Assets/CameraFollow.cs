using System.Linq;
using UnityEngine;

namespace Assets
{
	public class CameraFollow : MonoBehaviour
	{
		public Transform[] targets;
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
			var total = new Vector3();
			foreach (var target in targets)
				total += target.position;

			transform.position = (total / targets.Length) + Vector3.back * distance;

			var targetDistance = targets.Max(t1 => targets.Max(t2 => Vector3.Distance(t1.position, t2.position)));
			camera.orthographicSize = Mathf.Clamp(targetDistance / 2f, startingSize, maxCameraSize);
		}
	}
}