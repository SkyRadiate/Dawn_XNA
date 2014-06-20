using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
    class GraphicsManager : EngineObject
    {
        public override string ObjectClassName() { return Define.EngineClassName.GraphicsManager(); }

        private SceneManager _Scenes;
        public SceneManager Scenes { get { return _Scenes; } }
        public GraphicsManager()
        {
            _Scenes=new SceneManager();
        }

        public void Initialize()
        {
            _Scenes.Initialize();
        }
    }
}
