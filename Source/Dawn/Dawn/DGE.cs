using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dawn.Engine.Manager;

namespace Dawn
{
	static public class DGE
	{

		public static void Initialize()
		{
			Engine = new Engine.Manager.EngineManager();
			Engine.Initialize();
		}
		public static AudioManager Audio { get { return Engine.Audio; } }
		public static DebugManager Debug { get { return Engine.Debug; } }
		public static GraphicsManager Graphics { get { return Engine.Graphics; } }
		public static InputManager Input { get { return Engine.Input; } }
		public static SceneManager Scenes { get { return Engine.Scenes; } }
		public static DataManager Data { get { return Engine.Data; } }
		public static EngineManager Engine { get; private set; }
		public static CacheManager Cache { get { return Engine.Cache; } }
		public static ThreadManager Threads { get { return Engine.Threads; } }
		public static GameDawn Game { get { return Engine.Game; } }
		public static void Update() { Engine.Update(); }
		
		public static void Run(Engine.Basic.Game GameObject)
		{
			Program.Run(GameObject);
		}
	}
}
