using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
    public class EngineManager : EngineObject
    {
        public override string ObjectClassName() { return Define.EngineClassName.EngineManager(); }

		public Dawn.Engine.Basic.Game GameObject { get { return Program.DawnGameObject; } }
		public AudioManager Audio { get; private set; }
		public DebugManager Debug { get; private set; }
		public GraphicsManager Graphics { get; private set; }
		public InputManager Input { get; private set; }
		public SceneManager Scenes { get { return Graphics.Scenes; } }
		public DataManager Data { get; private set; }
		public CacheManager Cache { get { return Data.Cache; } }
		public ThreadManager Threads { get; private set; }
		public TextureManager TextureCache { get; private set; }
		public GameDawn Game { get { return Program.Game; } }
		

		public event SimpleEventHandler Start;
        public EngineManager()
        {
            Audio = new AudioManager();
            Debug = new DebugManager();
            Graphics = new GraphicsManager();
            Input = new InputManager();
            Data = new DataManager();
			Threads = new ThreadManager();
			TextureCache = new TextureManager();
        }

		protected void OnStart()
		{
			GameObject.Main();
			if (Start != null)
			{
				EventArgs e=new EventArgs();
				Start(this, e);
			}
		}

		~EngineManager()
		{

		}

        public void Initialize()
        {
            Audio.Initialize();
            Debug.Initialize();
            Graphics.Initialize();
            Input.Initialize();
            Data.Initialize();
			Threads.Initialize();
			TextureCache.Initialize();
			OnStart();
        }
        public void Update()
        {
            Audio.Update();
			Graphics.Update();
        }
    }
}
