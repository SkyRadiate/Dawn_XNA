using System;

namespace Dawn
{
#if WINDOWS || XBOX
    static class Program
    {
		private static GameDawn _Game;
		public static GameDawn Game { get { return _Game; } }

        static void Main(string[] args)
        {
            using (GameDawn game = new GameDawn())
            {
				_Game = game;
                game.Run();
            }
        }
    }
#endif
}

