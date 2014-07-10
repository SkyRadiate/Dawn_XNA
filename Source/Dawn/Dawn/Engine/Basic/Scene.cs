using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dawn.Engine.Manager;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Dawn.Engine.Manager.SceneManager")]
namespace Dawn.Engine.Basic
{
	public class Scene : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.Scene(); }

		internal SceneManager Manager;
		
		
		public Scene()
		{
			
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
