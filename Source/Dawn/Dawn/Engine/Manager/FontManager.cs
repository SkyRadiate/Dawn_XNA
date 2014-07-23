using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
	public class FontManager : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.FontManager(); }
		
		public FontManager()
		{

		}

		public Dawn.Engine.Manager.Processor.FontManager.FontHelper GetHelper(Dawn.Engine.Resource.Font font)
		{
			return new Dawn.Engine.Manager.Processor.FontManager.FontHelper(font);
		}

		public void Initialize()
		{

		}
	}
}
