using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace Dawn.Engine.Manager
{
	class CacheManager : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.CacheManager(); }

		public void Initialize()
		{

		}

		public Texture2D Graphics(string filename)
		{
			Texture2D tex;
			tex = DGE.Data.Content.Load<Texture2D>(DGE.Data.Graphics(filename));
			return tex;
		}
	}
}
