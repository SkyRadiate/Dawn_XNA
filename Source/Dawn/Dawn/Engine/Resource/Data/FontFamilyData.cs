using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource.Data
{
	public class FontFamilyData
	{

		public override string ObjectClassName() { return Define.EngineClassName.FontFamilyData(); }
		public FontFamilyData(string filename)
			: this(new System.Drawing.FontFamily(""), 0, false, false, false)
		{
		}

		public FontFamilyData(System.Drawing.FontFamily families, int size, bool blod, bool italic, bool underline)
		{
			Family = families;
			Size = size;
			isBlod = blod;
			isItalic = italic;
			isUnderline = underline;
		}

		public System.Drawing.FontFamily Family { get; set; }
		public float Size { get; set; }
		public bool isBlod { get; set; }
		public bool isItalic { get; set; }
		public bool isUnderline { get; set; }
	}
}
