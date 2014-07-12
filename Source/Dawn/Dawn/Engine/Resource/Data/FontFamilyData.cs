using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource.Data
{
	public class FontFamilyData : EngineObject
	{

		public override string ObjectClassName() { return Define.EngineClassName.FontFamilyData(); }
		public FontFamilyData(string filename)
			: this(new System.Drawing.FontFamily(""), 0, System.Drawing.Color.White, false, false, false)
		{
		}

		public FontFamilyData(System.Drawing.FontFamily families, int size, System.Drawing.Color color, bool blod, bool italic, bool underline)
		{
			Family = families;
			Size = size;
			isBlod = blod;
			isItalic = italic;
			isUnderline = underline;
			Color = color;
		}

		public System.Drawing.FontFamily Family { get; set; }
		public float Size { get; set; }
		public bool isBlod { get; set; }
		public bool isItalic { get; set; }
		public bool isUnderline { get; set; }
		public System.Drawing.Color Color { get; set; }
	}
}
