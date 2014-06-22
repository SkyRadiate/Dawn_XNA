using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dawn.Engine;

namespace Dawn.Engine.Manager
{
	using GameConst_StartScene = Dawn.Game.Scene.Scene_Test;
    class SceneManager : EngineObject
    {
		List<Basic.Scene> SceneStack;
		
        public override string ObjectClassName() { return Define.EngineClassName.SceneManager(); }
        public SceneManager()
        {
			SceneStack = new List<Basic.Scene>();
        }

        public void Initialize()
        {
			SceneStack.Add(new GameConst_StartScene());
			ProcessStart();
        }

		public void Initialize(Basic.Scene scene)
		{
			SceneStack.Add(scene);
			ProcessStart();
		}

		public void Goto(Basic.Scene scene)
		{
			ProcessEnd();
			SceneStack.Clear();
			SceneStack.Add(scene);
			ProcessStart();
		}

		public void Retrun()
		{
			ProcessEnd();
			SceneStack.RemoveAt(SceneStack.Count - 1);
			ProcessStart();
		}
		public void Push(Basic.Scene scene)
		{
			ProcessEnd();
			SceneStack.Add(scene);
			ProcessStart();
		}

		public bool isEmpty()
		{
			return SceneStack.Count <= 0;
		}
		private void ProcessEnd()
		{
			if (SceneStack.Count > 0)
			{
				SceneStack.ElementAt(SceneStack.Count - 1).End();
			}
		}

		private void ProcessStart()
		{
			SceneStack.ElementAt(SceneStack.Count - 1).Initialize(this);
			if (SceneStack.Count > 0)
			{
				SceneStack.ElementAt(SceneStack.Count - 1).Start();
			}
		}

		public void Update()
		{
			SceneStack.ElementAt(SceneStack.Count - 1).Update();
		}

		public Basic.Scene NowScene()
		{
			return SceneStack.ElementAt(SceneStack.Count - 1);
		}
    }


}
