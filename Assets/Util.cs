using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
	public class Util
	{
		public static T RandomElement<T>(T[] array)
		{
			if (array == null)
				return default(T);

			if (!array.Any())
				return default(T);

			return array[Random.Range(0, array.Length)];
		}

		public static T RandomElement<T>(IEnumerable<T> list)
		{
			return RandomElement(list.ToArray());
		}

		public static bool Chance(float chance)
		{
			return Random.value <= chance;
		}

		public static Quaternion RandomRotation()
		{
			return Quaternion.Euler(0, 0, Random.value * 360f);
		}
	}
}