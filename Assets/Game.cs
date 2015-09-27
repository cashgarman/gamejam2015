using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
	public class Game : MonoBehaviour
	{
		public static Game instance;

		public Ship player1Ship;
		public Ship player2Ship;
		public float arenaSize = 100f;
		public CameraFollow followCamera;
		public Text player1HealthText;
		public Text player2HealthText;
		public Text player1SpeedText;
		public Text player2SpeedText;
		public int numWormholePairs = 10;
		public int numAsteroids = 30;
		private bool slowmo;
		private int player1Score;
		private int player2Score;
		public GameObject[] redRibbons;
		public GameObject[] blueRibbons;
		public GameObject player1WinObject;
		public GameObject player2WinObject;
		private bool gameover;
		public Slider player1HealthSlider;
		public Slider player2HealthSlider;
		public Slider player1EnergySlider;
		public Slider player2EnergySlider;

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
			if(Input.GetKeyDown("1") && !Input.GetKey(KeyCode.LeftShift))
				OnPlayerWon(1);
			if (Input.GetKeyDown("2") && !Input.GetKey(KeyCode.LeftShift))
				OnPlayerWon(2);

			if (Input.GetKeyDown("1") && Input.GetKey(KeyCode.LeftShift))
				player1Ship.OnDestroyed();
			if (Input.GetKeyDown("2") && Input.GetKey(KeyCode.LeftShift))
				player2Ship.OnDestroyed();
			
			if(gameover && Input.GetKeyDown(KeyCode.Return))
				Application.LoadLevel(0);

			// Firing
			if (Input.GetButtonDown("Player1Fire"))
				player1Ship.StartFire();

			if (Input.GetButtonDown("Player2Fire"))
				player2Ship.StartFire();

			if (Input.GetButtonUp("Player1Fire"))
				player1Ship.EndFire();

			if (Input.GetButtonUp("Player2Fire"))
				player2Ship.EndFire();

			if (Input.GetKeyDown(KeyCode.Space))
			{
				slowmo = !slowmo;
				Time.timeScale = slowmo ? GameSettings.instance.slowmoSpeed : 1f;
			}

			// CHEATS
			if (Input.GetKeyDown(KeyCode.M))
			{
				Debug.Log("Giving both players missiles");
				player1Ship.InstallWeapon(GameSettings.instance.missileWeaponPrefab);
				player2Ship.InstallWeapon(GameSettings.instance.missileWeaponPrefab);
			}

			if (Input.GetKeyDown(KeyCode.B))
			{
				Debug.Log("Giving both players beam weapons");
				player1Ship.InstallWeapon(GameSettings.instance.beamWeaponPrefab);
				player2Ship.InstallWeapon(GameSettings.instance.beamWeaponPrefab);
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
			{
				player1Ship = (Instantiate(GameSettings.instance.player1ShipPrefab, GetRandomSpawnPosition(), Quaternion.identity) as GameObject).GetComponent<Ship>();
				player1Ship.InstallWeapon(GameSettings.instance.starterWeaponPrefab);
			}
			else
			{
				player2Ship = (Instantiate(GameSettings.instance.player2ShipPrefab, GetRandomSpawnPosition(), Quaternion.identity) as GameObject).GetComponent<Ship>();
				player2Ship.InstallWeapon(GameSettings.instance.starterWeaponPrefab);
			}
		}

		private Vector3 GetRandomSpawnPosition()
		{
			var radius = GameSettings.instance.spawnRadius;
			return new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius));
		}

		public void OnPlayerKilled(int playerNumber)
		{
			if (playerNumber == 1)
				player2Score++;
			else
				player1Score++;

			UpdateScores();
		}

		private void UpdateScores()
		{
			blueRibbons[0].SetActive(player1Score > 0);
			blueRibbons[1].SetActive(player1Score > 1);
			blueRibbons[2].SetActive(player1Score > 2);

			redRibbons[0].SetActive(player2Score > 0);
			redRibbons[1].SetActive(player2Score > 1);
			redRibbons[2].SetActive(player2Score > 2);

			// Check if someone won
			if (player1Score == 3)
				OnPlayerWon(1);
			else if(player2Score == 3)
				OnPlayerWon(2);
		}

		private void OnPlayerWon(int playerNumber)
		{
			// Disable the player controls
			player1Ship.SetDisabled(true);
			player2Ship.SetDisabled(true);

			// Show the win message
			if (playerNumber == 1)
				player1WinObject.SetActive(true);
			else
				player2WinObject.SetActive(true);
				
			gameover = true;
		}

		public static SpaceObject GetOtherShip(Ship ship)
		{
			return ship == instance.player1Ship ? instance.player2Ship : instance.player1Ship;
		}

		public void DelaySpawnPlayer(int playerNumber)
		{
			StartCoroutine(DelaySpawnPlayerInternal(playerNumber));
		}

		private IEnumerator DelaySpawnPlayerInternal(int playerNumber)
		{
			yield return new WaitForSeconds(GameSettings.instance.respawnDelay);

			SpawnPlayer(playerNumber);
		}
	}
}