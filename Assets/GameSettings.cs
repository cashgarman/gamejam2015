using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public static GameSettings instance;

	public GameObject wormholePrefab;
	public GameObject player1ShipPrefab;
	public GameObject player2ShipPrefab;

	public void Awake()
	{
		instance = this;
	}
}