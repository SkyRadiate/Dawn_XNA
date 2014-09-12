using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource
{
	public class Font : Resource
	{
		public override string ObjectClassName() { return Define.EngineClassName.FontResource(); }

		private System.Drawing.Font _font;
		private System.Drawing.Graphics graphics;
		private System.Drawing.Size proposedSize = new System.Drawing.Size(int.MaxValue, int.MaxValue);

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
			_font = new System.Drawing.Font(_fontData.Family, _fontData.Size, style, System.Drawing.GraphicsUnit.Pixel);
		}
		public float GetPixelConvert()
		{
			return _fontData.Size / _fontData.Family.GetEmHeight(_font.Style);
		}
		public System.Drawing.Font GetFont()
		{
			return _font;
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
			return System.Windows.Forms.TextRenderer.MeasureText(graphics, character, _font, proposedSize, System.Windows.Forms.TextFormatFlags.NoPadding).Width;
			//return graphics.MeasureString(character, _font).Width;//, new System.Drawing.SizeF(MaxCharacterWidth(), MaxCharacterHeight())).Width;
		}

		public float CharacterHeight(string character)
		{
			return System.Windows.Forms.TextRenderer.MeasureText(graphics, character, _font, proposedSize, System.Windows.Forms.TextFormatFlags.NoPadding).Height;
			//return graphics.MeasureString(character, _font).Height;//, new System.Drawing.SizeF(MaxCharacterWidth(), MaxCharacterHeight())).Height;
		}

		public override object Clone()
		{
			Font res = new Font();
			res._isLoad = true;
			res.graphics = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1));
			res.graphics.PageUnit = System.Drawing.GraphicsUnit.Pixel;
			res._font = (System.Drawing.Font)_font.Clone();
			res._fontData = (Data.FontFamilyData)_fontData.Clone();
			return res;
		}
	}

}
