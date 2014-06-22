using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dawn.Engine.Manager;

namespace Dawn.Engine.Basic
{
	class Scene : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.Scene(); }
		protected SceneManager Manager;
		public Scene()
		{
			
		}

		public void Initialize(SceneManager _Manager)
		{
			Manager = _Manager;
		}
		public virtual void Start()
		{

		}

		public virtual void End()
		{

		}

		public virtual void Update()
		{

		}
	}
}
