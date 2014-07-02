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
		private System.Drawing.Graphics graphics;

		protected Data.FontFamilyData _fontData;
		public Font()
            : base()
        {
			_fontData = null;
        }
        public Font(string filename)
            : base(filename)
        {
			_fontData = new Data.FontFamilyData(filename);
        }

		public Font(Data.FontFamilyData fontData)
			: base()
		{
			_fontData = fontData;
		}
		~Font()
        {
            Dispose();
        }

		public Data.FontFamilyData font { get { return _fontData; } }
		public override void Load()
		{
			base.Load();
			graphics = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1));
			graphics.PageUnit = System.Drawing.GraphicsUnit.Pixel;

			System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
			if(_fontData.isBlod)
			{
				style |= System.Drawing.FontStyle.Bold;
			}
			if(_fontData.isItalic)
			{
				style |= System.Drawing.FontStyle.Italic;
			}
			if(_fontData.isUnderline)
			{
				style |= System.Drawing.FontStyle.Underline;
			}
			_font = new System.Drawing.Font(_fontData.Family, _fontData.Size, style, System.Drawing.GraphicsUnit.Pixel);

			
			//_font.
		}
		public float GetPixelConvert()
		{
			return _fontData.Size / _fontData.Family.GetEmHeight(_font.Style);
		}
		public override void Unload()
		{
			_font = null;
			graphics = null;
			base.Unload();
		}
		public float MaxCharacterWidth()
		{
			return _fontData.Size;
		}

		public float MaxCharacterHeight()
		{
			return (_fontData.Family.GetCellAscent(_font.Style) + _fontData.Family.GetCellDescent(_font.Style)) * GetPixelConvert();
		}

		public float CharacterWidth(string character)
		{
			return graphics.MeasureString(character, _font).Width;
		}

		public float CharacterHeight(string character)
		{
			return graphics.MeasureString(character, _font).Height;
		}
	}
}
