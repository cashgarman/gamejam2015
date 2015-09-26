using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public static GameSettings instance;

	public GameObject exitPrefab;
	public float movementSpeed = 10f;
	public GameObject player;
	public float boostFactor = 5f;

	public void Awake()
	{
		instance = this;
	}
}