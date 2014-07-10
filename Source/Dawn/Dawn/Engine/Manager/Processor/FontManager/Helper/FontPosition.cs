using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager.Processor.FontManager.Helper
{
	public class FontPosition : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.FontPosition(); }

		public int TexID { get; set; }
		public int Row { get; set; }
		public int Col { get; set; }
	}
}
