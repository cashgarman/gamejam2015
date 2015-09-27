using UnityEngine;

namespace Assets
{
	public class SpaceObject : MonoBehaviour
	{
		public float wormholeImmuneTime;
		public bool immuneToWormholes;

		public void Update()
		{
			if (wormholeImmuneTime > 0)
			{
				wormholeImmuneTime -= Time.deltaTime;
				if (wormholeImmuneTime <= 0f)
					immuneToWormholes = false;
			}
		}
	}
}