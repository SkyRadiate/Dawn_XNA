using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dawn.Engine.Resource;

namespace Dawn.Engine.Manager
{
	public class CacheManager : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.CacheManager(); }

		private FontManager _fontManager;
		public CacheManager()
		{
			_fontManager = new FontManager();
		}
		public void Initialize()
		{
			_fontManager.Initialize();
		}
		public FontManager Fonts { get { return _fontManager; } }
		public Texture Graphics(string filename)
		{
			Texture tex = new Texture(DGE.Data.Graphics(filename));
			tex.Load();
			return tex;
		}
	}
}
