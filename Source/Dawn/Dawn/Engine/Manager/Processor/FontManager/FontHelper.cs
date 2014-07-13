using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dawn.Engine.Define;
using Dawn.Engine;

namespace Dawn.Engine.Manager.Processor.FontManager
{
	public class FontHelper : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.FontHelper(); }

		protected Texture2D[] tex;
		Dictionary<string, FontManager.Helper.CharacterObject> characters;
		Dictionary<FontManager.Helper.FontPosition, string> bcharacters;
		protected Resource.Font _font;

		protected int texRow;
		protected int texCol;

		protected float texRowPixels, texColPixels;

		protected System.Drawing.Graphics graphics;
		protected System.Drawing.Brush brush;
		protected System.Drawing.Bitmap bitmap;
		protected System.Windows.Forms.TextRenderer renderer;
		protected System.Drawing.IDeviceContext hdc;
		private Random random;
		//protected 
		public FontHelper(Resource.Font font)
		{
			_font = font;

			texRowPixels = _font.MaxCharacterHeight();
			texColPixels = _font.MaxCharacterWidth();
			texCol = (int)(EngineConst.FontHelper_TextureWidth() / _font.MaxCharacterWidth());
			texRow = (int)(EngineConst.FontHelper_TextureHeight() / _font.MaxCharacterHeight());

			tex = new Texture2D[EngineConst.FontHelper_TextureNum()];
			for (int i = 0; i < EngineConst.FontHelper_TextureNum(); i++)
			{
				tex[i] = new Texture2D(DGE.Graphics.Device, EngineConst.FontHelper_TextureWidth(), EngineConst.FontHelper_TextureHeight());
			}
			characters = new Dictionary<string, Helper.CharacterObject>(texCol * texRow * EngineConst.FontHelper_TextureNum());
			bcharacters = new Dictionary<Helper.FontPosition, string>(texCol * texRow * EngineConst.FontHelper_TextureNum(), new Helper.FontPositionComparer());
			//bitmap = new System.Drawing.Bitmap(EngineConst.FontHelper_TextureWidth(), EngineConst.FontHelper_TextureHeight());
			brush = new System.Drawing.SolidBrush(_font.font.Color);
			random = new Random();
		}

		protected Texture2D FillTexture(ref System.Drawing.Bitmap bitmap)
		{
			Texture2D tmpTex = new Texture2D(DGE.Graphics.Device, (int)bitmap.Width, (int)bitmap.Height);
			Color[] colorMap = new Color[tmpTex.Width * tmpTex.Height];
			for (int i = 0; i < bitmap.Height; i++)
			{
				for (int j = 0; j < bitmap.Width; j++)
				{
					System.Drawing.Color color = bitmap.GetPixel(j, i);
					colorMap[i * tmpTex.Width + j] = new Color(color.R, color.G, color.B, color.A);
				}
			}
			tmpTex.SetData<Color>(colorMap);
			return tmpTex;
		}

		protected void setGraphics(ref System.Drawing.Bitmap bitmap)
		{
			graphics = System.Drawing.Graphics.FromImage(bitmap);
			//graphics.PageUnit = System.Drawing.GraphicsUnit.Pixel;
			graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
		}
		protected virtual Texture2D _DrawCharacter(string character,Helper.CharacterObject obj)
		{
			System.Drawing.Bitmap tmpBitmap = new System.Drawing.Bitmap((int)texColPixels, (int)texRowPixels);
			setGraphics(ref tmpBitmap);
			graphics.Clear(System.Drawing.Color.Transparent);
			//graphics.DrawString(character, _font.GetFont(), brush, (float)position.X, (float)position.Y);
			System.Windows.Forms.TextRenderer.DrawText(graphics, character, _font.GetFont(), new System.Drawing.Point(0, 0), _font.font.Color, System.Windows.Forms.TextFormatFlags.NoPadding);

			return FillTexture(ref tmpBitmap);
		}
		protected void _NewCharacter(string character, Helper.CharacterObject obj)
		{
			GraphicsDevice graphicsDevice = DGE.Graphics.Device;
			RenderTarget2D rt = new RenderTarget2D(graphicsDevice, EngineConst.FontHelper_TextureWidth(),EngineConst.FontHelper_TextureHeight());

			RenderTargetBinding[] old = graphicsDevice.GetRenderTargets();

			graphicsDevice.SetRenderTarget(rt);

			graphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 0, 0);

			SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
			spriteBatch.Begin();

			spriteBatch.Draw(tex[obj.position.TexID], new Vector2(0, 0), Color.White);
			spriteBatch.Draw(_DrawCharacter(character, obj), new Vector2(obj.position.Col * texColPixels, obj.position.Row * texRowPixels), Color.White);

			spriteBatch.End();
			spriteBatch.Dispose();

			graphicsDevice.SetRenderTargets(old);


			graphicsDevice.Clear(Define.GameWindow.BackgroundColor());

			RenderTargetBinding binding=new RenderTargetBinding(rt);
			tex[obj.position.TexID] = binding.RenderTarget as Texture2D;
		}

		protected void NewCharacter(string character, Helper.CharacterObject obj)
		{
			_NewCharacter(character, obj);
		}

		protected Helper.CharacterObject FindCharacter(string character)
		{
			Helper.CharacterObject obj;
			characters.TryGetValue(character, out obj);
			return obj;
		}

		protected Helper.CharacterObject GenerateCharacter(string character)
		{
			Helper.CharacterObject obj;
			obj = FindCharacter(character);
			if(obj==null)
			{
				obj = new Helper.CharacterObject { position = null, Width = -1, Height = -1, character = character };
				characters.Add(character, obj);
			}
			return obj;
		}
		protected Helper.CharacterObject _GetCharacter(string character)
		{
			Helper.CharacterObject obj;
			obj = GenerateCharacter(character);
			if (obj.position == null)
			{
				Helper.FontPosition pos = new Helper.FontPosition();

				pos.TexID = random.Next(0, EngineConst.FontHelper_TextureNum()-1);
				pos.Row = random.Next(0, texRow - 1);
				pos.Col = random.Next(0, texCol - 1);
				Helper.FontPosition tmp = new Helper.FontPosition { TexID = pos.TexID, Row = pos.Row, Col = pos.Col };
				bool useFlag = false;
				Helper.FontPosition tmp2 = new Helper.FontPosition { TexID = pos.TexID, Row = pos.Row, Col = pos.Col };
				while (bcharacters.ContainsKey(pos))
				{
					pos.Col++;
					if(pos.Col>=texCol)
					{
						pos.Col = 0;
						pos.Row++;
					}
					if(pos.Row>=texRow)
					{
						pos.Row = 0;
						pos.TexID++;
					}
					if(pos.TexID>=EngineConst.FontHelper_TextureNum())
					{
						pos.TexID = 0;
					}
					if(tmp.Row==pos.Row && tmp.Col==pos.Col && tmp.TexID==pos.TexID)
					{
						string tmpChar;
						bcharacters.TryGetValue(pos, out tmpChar);
						bcharacters.Remove(pos);
						bcharacters.Add(pos, character);
						characters.Remove(tmpChar);
						useFlag = true;
						break;
					}
				}
				if(!useFlag)
				{
					bcharacters.Add(pos, character);
				}
				obj.position = pos;
				NewCharacter(character, obj);
			}
			return obj;
		}

		protected void DrawCharacterToTexture(int x, int y, string character, SpriteBatch canvas)
		{
			Helper.FontPosition pos = _GetCharacter(character).position;
			canvas.Draw(tex[pos.TexID], new Vector2(x, y), new Rectangle((int)(pos.Col * texColPixels), (int)(pos.Row * texRowPixels), (int)texColPixels, (int)texRowPixels), Color.White);
		}

		protected float[] MeasureString(string str)
		{
			float[] widths;
			widths = new float[str.Length];
			for (int i = 0; i < str.Length; i++)
			{
				Helper.CharacterObject obj = GenerateCharacter(str.Substring(i, 1));
				float tmpWidth;
				if (obj.Width == -1)
				{
					tmpWidth = _font.CharacterWidth(str.Substring(i, 1));
					obj.Width = tmpWidth;
				}
				else
				{
					tmpWidth = obj.Width;
				}
				if(obj.Height==-1)
				{
					obj.Height = _font.CharacterWidth(str.Substring(i, 1));
				}

				widths[i] = tmpWidth;
			}
			return widths;
		}
		public Texture2D DrawStringToTexture(string str)
		{
			//System.Diagnostics.Trace.WriteLine("Dawn> Render String...Setting Target");
			float[] strWidth = MeasureString(str);
			
			GraphicsDevice graphicsDevice = DGE.Graphics.Device;
			RenderTarget2D rt = new RenderTarget2D(graphicsDevice, (int)strWidth.Sum(), (int)_font.MaxCharacterHeight());

			RenderTargetBinding[] old = graphicsDevice.GetRenderTargets();
			
			graphicsDevice.SetRenderTarget(rt);
			
			graphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 0, 0);
			//System.Diagnostics.Trace.WriteLine("Dawn> Render String...Processing");
			SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
			spriteBatch.Begin();

			float x = 0;
			for (int i = 0; i < str.Length; i++)
			{
				//System.Diagnostics.Trace.WriteLine("Dawn> Render String...Character #" + i.ToString());
				DrawCharacterToTexture((int)x, 0, str.Substring(i, 1), spriteBatch);
				x += strWidth[i];
			}

			spriteBatch.End();
			spriteBatch.Dispose();
			
			graphicsDevice.SetRenderTargets(old);
			

			graphicsDevice.Clear(Define.GameWindow.BackgroundColor());

			RenderTargetBinding binding=new RenderTargetBinding(rt);
			return binding.RenderTarget as Texture2D;
			
		}

		public void DrawString(string str, int x, int y)
		{
			float x1 = x;
			float[] strWidth = MeasureString(str);
			for (int i = 0; i < str.Length; i++)
			{
				//System.Diagnostics.Trace.WriteLine("Dawn> Render String...Character #" + i.ToString());
				DrawCharacterToTexture((int)x1, y, str.Substring(i, 1), DGE.Graphics.Canvas);
				x1 += strWidth[i];
			}
		}

		public void DrawStringCommand(string str, int x, int y)
		{
			float y1 = y;
			float y1Add=_font.MaxCharacterHeight();
			char[] ch=new char[]{'\n','\r'};
			string[] s = str.Split(ch);
			for (int i = 0; i < s.Length; i++)
			{
				DrawString(s[i], x, (int)y1);
				y1 += y1Add;
			}
		}
	}
}
