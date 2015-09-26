using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public static GameSettings instance;

	public GameObject wormholePrefab;
	public GameObject playerShipPrefab;

	public void Awake()
	{
		instance = this;
	}
}