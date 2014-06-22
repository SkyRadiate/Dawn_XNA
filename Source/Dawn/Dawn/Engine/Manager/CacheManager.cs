using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dawn.Engine.Resource;

namespace Dawn.Engine.Manager
{
	class CacheManager : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.CacheManager(); }

		public void Initialize()
		{

		}

		public Texture Graphics(string filename)
		{
			Texture tex = new Texture(DGE.Data.Graphics(filename));
			tex.Load();
			return tex;
		}
	}
}
