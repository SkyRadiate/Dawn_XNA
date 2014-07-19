using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic
{
	public class Sprite : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.Sprite(); }

		public float X { get; set; }
		public float Y { get; set; }
		public int Z { get; set; }
		public Dawn.Engine.Resource.Texture tex { get; set; }

		public Sprite()
		{

		}
	}
}
