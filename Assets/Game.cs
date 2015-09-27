﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
	public class Game : MonoBehaviour
	{
		public static Game instance;

		public Ship player1Ship;
		public Ship player2Ship;
		public float arenaSize = 100f;
		public float spawnRadius = 20f;
		public CameraFollow followCamera;
		public Text player1HealthText;
		public Text player2HealthText;
		public Text player1SpeedText;
		public Text player2SpeedText;
		public int numWormholePairs = 10;
		public int numAsteroids = 30;
		private bool slowmo;

		public void Awake()
		{
			instance = this;
		}

		public void Start()
		{
			// Spawn wormholes
			for (var i = 0; i < numWormholePairs; ++i)
			{
				var sideAObject = Instantiate(GameSettings.instance.wormholePrefab, Random.insideUnitCircle * arenaSize, Quaternion.identity) as GameObject;
				var sideBObject = Instantiate(GameSettings.instance.wormholePrefab, Random.insideUnitCircle * arenaSize, Quaternion.identity) as GameObject;
				var sideA = sideAObject.GetComponent<Wormhole>();
				var sideB = sideBObject.GetComponent<Wormhole>();

				sideA.otherSide = sideB;
				sideB.otherSide = sideA;
			}

			// Spawn asteroids
			for (var i = 0; i < numAsteroids; ++i)
				Asteroid.Spawn(Random.value, Random.insideUnitCircle * arenaSize, Random.insideUnitCircle * GameSettings.instance.maxAsteroidStartingVelocity);

			// Spawn the players
			SpawnPlayer(1);
			SpawnPlayer(2);
		}

		public void Update()
		{
			HandleInput();
		}

		private void HandleInput()
		{
			// Firing
			if (Input.GetButtonDown("Player1Fire"))
				player1Ship.Fire();

			if (Input.GetButtonDown("Player2Fire"))
				player2Ship.Fire();

			if (Input.GetKeyDown(KeyCode.Space))
			{
				slowmo = !slowmo;
				Time.timeScale = slowmo ? GameSettings.instance.slowmoSpeed : 1f;
			}
		}

		public void SpawnPlayer(int playerNumber)
		{
			Debug.Log("Spawning player " + playerNumber);

			var position = new Vector3();
			do
			{
				position = GetRandomSpawnPosition();
			} while (Physics2D.OverlapCircle(position, 3f));

			if (playerNumber == 1)
				player1Ship = (Instantiate(GameSettings.instance.player1ShipPrefab, GetRandomSpawnPosition(), Quaternion.identity) as GameObject).GetComponent<Ship>();
			else
				player2Ship = (Instantiate(GameSettings.instance.player2ShipPrefab, GetRandomSpawnPosition(), Quaternion.identity) as GameObject).GetComponent<Ship>();
		}

		private Vector3 GetRandomSpawnPosition()
		{
			return new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius));
		}
	}
}