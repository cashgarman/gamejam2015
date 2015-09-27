using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public static GameSettings instance;

	public GameObject wormholePrefab;
	public GameObject player1ShipPrefab;
	public GameObject player2ShipPrefab;
	public GameObject asteroidPrefab;
	public GameObject explosionPrefab;
	public GameObject starterWeaponPrefab;

	public float boostEngineParticleSize = 1.5f;
	public float normalEngineParticleSize = 0.64f;
	public float boostEngineEmissionRate = 100f;
	public float normalEngineEmissionRate = 40f;
	public float boostFactor = 5f;
	public float wormholeImmunityTime = 1f;
	public float maxAsteroidStartingVelocity = 1f;
	public float maxAsteroidDebrisVelocity = 5f;
	public float minAsteroidScale = 0.5f;
	public float maxAsteroidScale = 4f;
	public float minAsteroidMass = 1f;
	public float maxAsteroidMass = 8f;
	public float minAsteroidImpactDamage = 0f;
	public float maxAsteroidImpactDamage = 50f;
	public float maxShipSpeed = 50f;
	public float minImpactDamageSpeed = 10f;
	public float asteroidExplosionSize = 100f;
	public float explosionDuration = 1f;
	public float slowmoSpeed = 0.1f;
	public float shipExplosionSize = 10f;
		
	public void Awake()
	{
		instance = this;
	}
}