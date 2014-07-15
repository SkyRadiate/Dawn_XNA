using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dawn.Engine;

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


		public ResourceClass GetResource<ResourceClass>(string _filename)
			where ResourceClass : Resource.Resource, new()
		{
			ResourceClass res = new ResourceClass();
			res.filename = _filename;
			res.Load();
			return res;
		}
		protected ResourceClass GetResourceMultiThread<ResourceClass>(string _filename)
			where ResourceClass : Resource.Resource, new()
		{
			ResourceClass res = new ResourceClass();
			Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor processor = new Basic.ThreadProcessor.ResourceLoadProcessor(res);
			res.filename = _filename;
			DGE.Threads.NewThread(processor);
			while (!DGE.Threads.isEnd(processor)) ;
			return res;
		}

		protected Resource.Font GetFont(Resource.Data.FontFamilyData fontData)
		{
			Resource.Font res = new Resource.Font(fontData);
			res.Load();
			return res;
		}
		public Resource.Texture Graphics(string filename, bool multiThread = true)
		{
			if (multiThread) return GetResourceMultiThread<Resource.Texture>(filename);
			return GetResource<Resource.Texture>(filename);
		}

		public Resource.Font Font(Resource.Data.FontFamilyData fontData)
		{
			return GetFont(fontData);
		}
		public Resource.Audio Audio(string filename, bool multiThread = true)
		{
			if (multiThread) return GetResourceMultiThread<Resource.Audio>(filename);
			return GetResource<Resource.Audio>(filename);
		}
		public Resource.Audio AudioStream(string filename, bool multiThread = true)
		{
			if (multiThread) return GetResourceMultiThread<Resource.AudioStream>(filename);
			return GetResource<Resource.AudioStream>(filename);
		}
		public Resource.LyricFile LyricFile(string filename, bool multiThread = true)
		{
			if (multiThread) return GetResourceMultiThread<Resource.LyricFile>(filename);
			return GetResource<Resource.LyricFile>(filename);
		}
	}
}
