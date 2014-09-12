using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager.Processor.FontManager.Helper
{
	public class CharacterObject : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.CharacterObject(); }
		public FontPosition position { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }
		public string character { get; set; }
	}
}
