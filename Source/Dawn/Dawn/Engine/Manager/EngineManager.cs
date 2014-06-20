using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
    class EngineManager : EngineObject
    {
        public override string ObjectClassName() { return Define.EngineClassName.EngineManager(); }
        private AudioManager _Audio;
        private DebugManager _Debug;
        private GraphicsManager _Graphics;
        private InputManager _Input;

        public AudioManager Audio { get { return _Audio;} }
        public DebugManager Debug { get { return _Debug; } }
        public GraphicsManager Graphics { get { return _Graphics; } }
        public InputManager Input { get { return _Input; } }
        public SceneManager Scenes { get { return _Graphics.Scenes; } }
        public EngineManager()
        {
            _Audio = new AudioManager();
            _Debug = new DebugManager();
            _Graphics = new GraphicsManager();
            _Input = new InputManager();
        }

        public void Initialize()
        {
            _Audio.Initialize();
            _Debug.Initialize();
            _Graphics.Initialize();
            _Input.Initialize();
        }

        public void Update()
        {

        }
    }
}
