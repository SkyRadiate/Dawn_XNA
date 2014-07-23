using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic
{
	class Bitmap : EngineObject
	{
		protected Dawn.Engine.Resource.Texture Tex;
		public override string ObjectClassName() { return Define.EngineClassName.Bitmap(); }

		public Bitmap(Dawn.Engine.Resource.Texture tex)
		{
			Tex = tex;
		}


	}
}
