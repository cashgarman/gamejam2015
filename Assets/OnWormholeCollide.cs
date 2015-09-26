using UnityEngine;

namespace Assets
{
	public class OnWormholeCollide : MonoBehaviour
	{
		public string nextLevel;

		public void OnTriggerEnter2D(Collider2D other)
		{
			// If the player hit the wormhole
//			if (other.gameObject == GameSettings.instance.player)
//			{
//				// Load the next level
//				Debug.Log("Loading " + nextLevel);
//				Application.LoadLevel(nextLevel);
//			}
		}
	}
}
