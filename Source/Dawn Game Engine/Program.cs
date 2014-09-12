using System;

namespace Dawn
{
    static class Program
    {
		private static GameDawn _Game;
		private static Dawn.Engine.Basic.Game GameObj;
		public static GameDawn Game { get { return _Game; } }
		public static Dawn.Engine.Basic.Game DawnGameObject { get { return GameObj; } }
		static public void Run(Dawn.Engine.Basic.Game GameObject)
		{
			using (GameDawn game = new GameDawn())
			{
				_Game = game;
				GameObj = GameObject;
				game.Run();
			}
		}
    }
}

