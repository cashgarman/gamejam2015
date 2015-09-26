using UnityEngine;

public class Level : MonoBehaviour
{
	public int numExits = 1;
	public float exitDistance = 100f;

	public void Start()
	{
		// Spawn portals to exit the level
		for (var i = 0; i < numExits; ++i)
		{
			var exitDirection = Random.insideUnitCircle;
//			exitDirection.Normalize();

			var exitObject = Instantiate(GameSettings.instance.wormholePrefab, exitDirection * exitDistance, Quaternion.identity) as GameObject;
//			Debug.Log("Spawning an exit at " + exitObject.transform.position, exitObject);
		}
	}
}