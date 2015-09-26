using UnityEngine;

namespace Assets
{
	public class CameraFollow : MonoBehaviour
	{
		public Transform target;
		public float distance;

		public void LateUpdate()
		{
			transform.position = target.position + Vector3.back * distance;
		}
	}
}