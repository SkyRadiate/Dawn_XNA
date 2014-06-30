using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource
{
	public class Font : Resource
	{
		public override string ObjectClassName() { return Define.EngineClassName.FontResource(); }

		public static byte MaxFontSize = 72;
		public static byte DefaultFontSize = 16;
		private System.Drawing.Font _font;

		protected Data.FontData _fontData;
		public Font()
            : base()
        {
			_fontData = null;
        }
        public Font(string filename)
            : base(filename)
        {
			_fontData = new Data.FontData(filename);
        }

		public Font(Data.FontData fontData)
			: base()
		{
			_fontData = fontData;
		}
		~Font()
        {
            Dispose();
        }

		public Data.FontData font { get { return _fontData; } }

		public int MaxCharacterWidth()
		{
			return 1;
		}

		public int MaxCharacterHeight()
		{
			return 1;
		}

		public int CharacterWidth(string character)
		{
			return 1;
		}

		public int CharacterHeight(string character)
		{
			return 1;
		}
	}
}
