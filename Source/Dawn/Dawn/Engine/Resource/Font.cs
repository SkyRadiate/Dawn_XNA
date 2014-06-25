using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource
{
	public class Font : Resource
	{
		public static byte MaxFontSize = 72;
		public static byte DefaultFontSize = 16;
		private System.Drawing.Font _font;
		public Font()
            : base()
        {
        }
        public Font(string filename)
            : base(filename)
        {
        }
		~Font()
        {
            Dispose();
        }
		public string name
		{
			get { return _font.Name; }
		}
		public int size
		{
			get { return (int)_font.Size; }
		}
		public bool isBlod
		{
			get { return _font.Bold; }
		}
		public bool isItalic
		{
			get { return _font.Italic; }
		}
		public bool isUnderline
		{
			get { return _font.Underline; }
		}
	}
}
