using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource.Data
{
	public class FontData : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.FontData(); }
		public FontData(string filename)
			: this("", 0, false, false, false)
		{
		}

		public FontData(string name, int size, bool blod, bool italic, bool underline)
		{
			Name = name;
			Size = size;
			isBlod = blod;
			isItalic = italic;
			isUnderline = underline;
		}

		public string Name { get; set; }
		public int Size { get; set; }
		public bool isBlod { get; set; }
		public bool isItalic { get; set; }
		public bool isUnderline { get; set; }


	}
}
