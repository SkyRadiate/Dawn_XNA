using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dawn.Engine.Manager;

namespace Dawn
{    
    static class DGE
    {
		
        private static Engine.Manager.EngineManager engine;
        public static void Initialize()
        {
            engine = new Engine.Manager.EngineManager();
            engine.Initialize();
        }
        public static AudioManager Audio { get { return engine.Audio; } }
        public static DebugManager Debug { get { return engine.Debug; } }
        public static GraphicsManager Graphics { get { return engine.Graphics; } }
        public static InputManager Input { get { return engine.Input; } }
        public static SceneManager Scenes { get { return engine.Scenes; } }
        public static DataManager Data { get { return engine.Data; } }
		public static EngineManager Engine { get { return engine; } }
		public static CacheManager Cache { get { return engine.Cache; } }

		public static GameDawn Game { get { return engine.Game; } }
        public static void Update() { engine.Update(); }
    }

    
}
