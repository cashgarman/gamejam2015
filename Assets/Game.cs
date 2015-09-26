using UnityEngine;

namespace Assets
{
	public class Game : MonoBehaviour
	{
		public static Game instance;

		public Ship player1Ship;
		public Ship player2Ship;
		public float spawnRadius = 20f;

		public void Awake()
		{
			instance = this;
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
		}

		public void RespawnPlayer(int playerNumber)
		{
			if (playerNumber == 1)
			{
				player1Ship = (Instantiate(GameSettings.instance.playerShipPrefab, GetRandomSpawnPosition(), Quaternion.identity) as GameObject).GetComponent<Ship>();
				player1Ship.playerNumber = 1;
				player1Ship.controls.horizontalAxisName = "Player1Horizontal";
				player1Ship.controls.verticalAxisName = "Player1Vertical";
			}
			else
			{
				player1Ship = (Instantiate(GameSettings.instance.playerShipPrefab, GetRandomSpawnPosition(), Quaternion.identity) as GameObject).GetComponent<Ship>();
				player1Ship.playerNumber = 2;
				player1Ship.controls.horizontalAxisName = "Player2Horizontal";
				player1Ship.controls.verticalAxisName = "Player2Vertical";
			}
		}

		private Vector3 GetRandomSpawnPosition()
		{
			return new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius));
		}
	}
}