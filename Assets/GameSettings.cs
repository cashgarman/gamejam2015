using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public static GameSettings instance;

	public GameObject wormholePrefab;
	public GameObject player1ShipPrefab;
	public GameObject player2ShipPrefab;
	public GameObject asteroidPrefab;

	public float boostEngineParticleSize = 1.5f;
	public float normalEngineParticleSize = 0.64f;
	public float boostEngineEmissionRate = 100f;
	public float normalEngineEmissionRate = 40f;
	public float boostFactor = 5f;
	public float wormholeImmunityTime = 1f;
	public static float maxAsteroidVelocity = 1f;
	public float minAsteroidScale = 0.5f;
	public float maxAsteroidScale = 4f;
	public float minAsteroidMass = 1f;
	public float maxAsteroidMass = 8f;

	public void Awake()
	{
		instance = this;
	}
}