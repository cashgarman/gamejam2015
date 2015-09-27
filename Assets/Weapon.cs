using UnityEngine;

namespace Assets
{
	public class Weapon : MonoBehaviour
	{
		public Ship ship;

		public virtual void StartFire()
		{
		}

		public virtual void EndFire()
		{
		}
	}
}