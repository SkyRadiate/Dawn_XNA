using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dawn.Engine;

namespace Dawn.Engine.Manager
{
    public class SceneManager : EngineObject
    {
		List<Basic.Scene> SceneStack;
		
        public override string ObjectClassName() { return Define.EngineClassName.SceneManager(); }
        public SceneManager()
        {
			SceneStack = new List<Basic.Scene>();
        }

        public void Initialize()
        {
        }

		public void Goto<SceneClass>()
			where SceneClass : Basic.Scene, new()
		{
			ProcessEnd();
			SceneStack.Clear();
			SceneStack.Add(new SceneClass());
			ProcessStart();
		}

		public void Retrun()
		{
			ProcessEnd();
			SceneStack.RemoveAt(SceneStack.Count - 1);
			ProcessStart();
		}
		public void Push<SceneClass>()
			where SceneClass : Basic.Scene, new()
		{
			ProcessEnd();
			SceneStack.Add(new SceneClass());
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
			SceneStack.ElementAt(SceneStack.Count - 1).Manager = this;
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
