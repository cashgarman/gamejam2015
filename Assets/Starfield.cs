using UnityEngine;

namespace Assets
{
	public class Starfield : MonoBehaviour
	{
		private new MeshRenderer renderer;
		public float parallax = 10f;

		public void Awake()
		{
			renderer = GetComponent<MeshRenderer>();
		}

		public void Update()
		{
			var offset = renderer.material.mainTextureOffset;
			offset.x = transform.position.x / parallax;
			offset.y = transform.position.y / parallax;
			renderer.material.mainTextureOffset = offset;
		}
	}
}
