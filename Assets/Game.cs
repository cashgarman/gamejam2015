namespace Assets
{
	public class Game
	{
		public static Game instance;

		public Level startingLevel;
		public Level currentLevel;

		public void Awake()
		{
			instance = this;
		}
	}
}