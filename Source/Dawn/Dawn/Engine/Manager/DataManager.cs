using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace Dawn.Engine.Manager
{
	public class DataManager : EngineObject
    {
		private CacheManager _Cache;
		public CacheManager Cache { get { return _Cache; } }
		public override string ObjectClassName() { return Define.EngineClassName.DataManager(); }

		public Microsoft.Xna.Framework.Content.ContentManager Content { get { return DGE.Game._ContentManager; ; } }
        public DataManager()
        {
			_Cache = new CacheManager();
        }

        public void Initialize()
        {
			_Cache.Initialize();
        }

        public static string WorkingPath()
        {
            return Environment.CurrentDirectory;
        }

        public static string ContentPath()
        {
            return System.IO.Path.GetFullPath(WorkingPath() + @"..\..\..\Res");
        }

        public string Data(string filename)
        {
            return ContentPath() + @"\" + filename;
        }
        public string Graphics(string filename)
        {
            return Data(@"Graphics\" + filename);
        }

		public string Texture(string filename)
		{
			return Graphics(@"Texture\" + filename);
		}
		public string SystemTexture(string filename)
		{
			return Texture(@"System\" + filename);
		}
        public string Audio(string filename)
        {
            return Data(@"Audio\" + filename);
        }

    }
}
