using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets
{
	public class Asteroid : SpaceObject
	{
		public float scale;
		public int minNumDebrisPieces = 0;
		public int maxNumDebrisPieces = 10;

		public new void Start()
		{
			base.Start();

			var size = Mathf.Lerp(GameSettings.instance.minAsteroidScale, GameSettings.instance.maxAsteroidScale, scale);
			transform.localScale = new Vector3(size, size, size);

			// Set the initial health based on the asteroid's size
			health = startingHealth * scale;
		}

		public override void OnDestroyed()
		{
			base.OnDestroyed();

			// Spawn some pieces based on how big the asteroid was
			SpawnDebris();

			// Destroy the asteroid
			Destroy(gameObject);
		}

		private void SpawnDebris()
		{
			Debug.Log("Spawning " + numDebrisChunks + " chunks of scale " + scale / 3);

			// Don't spawn smaller asteroids if they will be too small
			if (numDebrisChunks == 0 || scale / numDebrisChunks < .05f)
				return;

			for (var i = 0; i < numDebrisChunks; ++i)
				Spawn(scale / numDebrisChunks, transform.position + Random.value * transform.localScale);
		}

		public static void Spawn(float scale, Vector3 position)
		{
			// Spawn the asteroid
			var asteroidObject = Instantiate(GameSettings.instance.asteroidPrefab, position, Quaternion.Euler(0, 0, Random.value * 360f)) as GameObject;
			var asteroid = asteroidObject.GetComponent<Asteroid>();

			// Give the asteroid a random scale
			asteroid.scale = scale;

			// Adjust the asteroid's mass based on its scale
			var rigidBody = asteroid.GetComponent<Rigidbody2D>();
			rigidBody.mass = Mathf.Lerp(GameSettings.instance.minAsteroidMass, GameSettings.instance.maxAsteroidMass, scale);

			// Give the asteroid a random velocity
			rigidBody.velocity = Random.insideUnitCircle * GameSettings.maxAsteroidVelocity;
		}
	}
}
