using System;

namespace Dawn
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameDawn game = new GameDawn())
            {
                game.Run();
            }
        }
    }
#endif
}

