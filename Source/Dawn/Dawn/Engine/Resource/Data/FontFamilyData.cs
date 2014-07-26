using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource.Data
{
	public class FontFamilyData : EngineObject, ICloneable
	{

		public override string ObjectClassName() { return Define.EngineClassName.FontFamilyData(); }
		public FontFamilyData(string filename)
			: this(new System.Drawing.FontFamily(filename), 0, System.Drawing.Color.White, false)
		{
		}

		public FontFamilyData(System.Drawing.FontFamily families, float size, System.Drawing.Color color, bool blod)
		{
			Family = families;
			Size = size;
			isBlod = blod;
			Color = color;
		}

		public System.Drawing.FontFamily Family { get; set; }
		public float Size { get; set; }
		public bool isBlod { get; set; }
		public System.Drawing.Color Color { get; set; }

		public object Clone()
		{
			System.Drawing.FontFamily family = new System.Drawing.FontFamily(Family.Name);
			return new FontFamilyData(family, Size, Color, isBlod);
		}
	}
}
