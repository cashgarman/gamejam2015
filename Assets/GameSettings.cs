﻿using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public static GameSettings instance;

	public GameObject wormholePrefab;
	public GameObject player1ShipPrefab;
	public GameObject player2ShipPrefab;
	public GameObject asteroidPrefab;
	public GameObject explosionPrefab;
	public GameObject shockwavePrefab;
	public GameObject starterWeaponPrefab;
	public GameObject missileWeaponPrefab;
	public GameObject beamWeaponPrefab;
	public GameObject wormholeEnterEffectPrefab;
	public GameObject wormholeExitEffectPrefab;

	public GameObject[] weapons;

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
	public float wormholeEffectDurationPrefab = 3f;
	public float wormholeMovementSpeed = 1f;
	public float wormholeExitThreshold = 1f;
	public float wormholeExitImpulse = 25f;
	public float energyRechargePerSecond = 0.1f;
	public float boostEnergyUseRate = 0.25f;
	public float spawnRadius = 100f;
	public float minIdlePitch = 0.5f;
	public float maxIdlePitch = 1.0f;
	public float engineIdleVolume = 1f;
	public float engineBoostVolume = 1f;
	public float minBoostPitch = .5f;
	public float maxBoostPitch = 1f;
	public float minAsteroidExplosionSound = .5f;
	public float maxAsteroidExplosionSound = 1f;
	public float boostStartVolume = 0.5f;
	public float shipShockwaveDuration = 1f;
	public float shipShockwaveSize = 5f;
	public float respawnDelay = 2f;
	public float minWormholeSpawnRadius = 20;
	public float maxWormholeSpawnRadius = 100;
	public int numWormholePairs = 3;

	public void Awake()
	{
		instance = this;
	}
}