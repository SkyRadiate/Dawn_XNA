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
		protected string[,,] used;

		protected Resource.Font _font;

		protected bool[] texUsed;

		protected int texRow;
		protected int texCol;

		protected float texRowPixels, texColPixels;

		protected System.Drawing.Graphics graphics;
		protected System.Drawing.Brush brush;
		protected System.Drawing.Bitmap bitmap;
		protected System.Windows.Forms.TextRenderer renderer;
		protected System.Drawing.IDeviceContext hdc;
		public FontHelper(Resource.Font font)
		{
			_font = font;

			texRowPixels = _font.MaxCharacterHeight();
			texColPixels = _font.MaxCharacterWidth();
			texCol = (int)(EngineConst.FontHelper_TextureWidth() / _font.MaxCharacterWidth());
			texRow = (int)(EngineConst.FontHelper_TextureHeight() / _font.MaxCharacterHeight());
			texUsed = new bool[EngineConst.FontHelper_TextureNum()];
			used = new string[EngineConst.FontHelper_TextureNum(), texRow, texCol];

			tex = new Texture2D[EngineConst.FontHelper_TextureNum()];
			for (int i = 0; i < EngineConst.FontHelper_TextureNum(); i++)
			{
				texUsed[i] = false;
				
				for (int x = 0; x < texRow; x++)
				{
					for (int y = 0; y < texCol; y++)
					{
						used[i, x, y] = "";
					}
				}
			}
			bitmap = new System.Drawing.Bitmap(EngineConst.FontHelper_TextureWidth(), EngineConst.FontHelper_TextureHeight());
			graphics = System.Drawing.Graphics.FromImage(bitmap);
			//graphics.PageUnit = System.Drawing.GraphicsUnit.Pixel;
			graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			brush = new System.Drawing.SolidBrush(_font.font.Color);
		}

		protected Texture2D FillTexture(ref System.Drawing.Bitmap bitmap)
		{
			Texture2D tmpTex = new Texture2D(DGE.Graphics.Device, (int)bitmap.Width, (int)bitmap.Height);
			Color[] colorMap = new Color[tmpTex.Width * tmpTex.Height];
			tmpTex.GetData<Color>(colorMap);
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
		protected virtual Texture2D _DrawCharacter(string character)
		{
			System.Drawing.Bitmap tmpBitmap = new System.Drawing.Bitmap((int)_font.CharacterWidth(character), (int)_font.CharacterHeight(character));
			setGraphics(ref tmpBitmap);
			graphics.Clear(System.Drawing.Color.Transparent);
			//graphics.DrawString(character, _font.GetFont(), brush, (float)position.X, (float)position.Y);
			System.Windows.Forms.TextRenderer.DrawText(graphics, character, _font.GetFont(), new System.Drawing.Point(0, 0), _font.font.Color, System.Windows.Forms.TextFormatFlags.NoPadding);

			return FillTexture(ref tmpBitmap);
		}
		protected void _NewCharacter(string character, Helper.FontPosition position)
		{
			GraphicsDevice graphicsDevice = DGE.Graphics.Device;
			RenderTarget2D rt = new RenderTarget2D(graphicsDevice, EngineConst.FontHelper_TextureWidth(),EngineConst.FontHelper_TextureHeight());

			RenderTargetBinding[] old = graphicsDevice.GetRenderTargets();

			graphicsDevice.SetRenderTarget(rt);

			graphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 0, 0);

			SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
			spriteBatch.Begin();

			spriteBatch.Draw(tex[position.TexID], new Vector2(0, 0), Color.White);
			spriteBatch.Draw(_DrawCharacter(character), new Vector2(position.Col * texColPixels, position.Row * texRowPixels), Color.White);

			spriteBatch.End();
			spriteBatch.Dispose();

			graphicsDevice.SetRenderTargets(old);


			graphicsDevice.Clear(Define.GameWindow.BackgroundColor());

			RenderTargetBinding binding=new RenderTargetBinding(rt);
			tex[position.TexID] = binding.RenderTarget as Texture2D;
		}

		protected Helper.FontPosition NewCharacter(string character)
		{
			Helper.FontPosition _pos;

			for (int i = 0; i < EngineConst.FontHelper_TextureNum(); i++)
			{
				if (texUsed[i] == true)
				{
					for (int x = 0; x < texRow; x++)
					{
						for (int y = 0; y < texCol; y++)
						{
							if (used[i, x, y] == "")
							{
								_pos = new Helper.FontPosition { TexID = i, Row = x, Col = y };
								used[i, x, y] = character;
								_NewCharacter(character, _pos);
								return _pos;
							}
						}
					}
				}
			}

			//Tex Full
			for (int i = 0; i < EngineConst.FontHelper_TextureNum(); i++)
			{
				if (texUsed[i] == false)
				{
					texUsed[i] = true;
					tex[i] = new Texture2D(DGE.Graphics.Device, EngineConst.FontHelper_TextureWidth(), EngineConst.FontHelper_TextureHeight());
					for (int x = 0; x < texRow; x++)
					{
						for (int y = 0; y < texCol; y++)
						{
							if (used[i, x, y] == "")
							{
								_pos = new Helper.FontPosition { TexID = i, Row = x, Col = y };
								used[i, x, y] = character;
								_NewCharacter(character, _pos);
								return _pos;
							}
						}
					}
				}
			}

			//All Full

			DGE.Debug.Error(this, "", "");
			return null;
		}

		protected Helper.FontPosition _GetCharacter(string character)
		{
			Helper.FontPosition _pos;

			for (int i = 0; i < EngineConst.FontHelper_TextureNum(); i++)
			{
				if (texUsed[i] == true)
				{
					for (int x = 0; x < texRow; x++)
					{
						for (int y = 0; y < texCol; y++)
						{
							if (used[i, x, y] == character)
							{
								_pos = new Helper.FontPosition { TexID = i, Row = x, Col = y };
								return _pos;
							}
						}
					}
				}
			}

			//Not Found
			return NewCharacter(character);
		}

		protected void DrawCharacterToTexture(int x, int y, string character, ref SpriteBatch canvas)
		{
			Helper.FontPosition pos = _GetCharacter(character);
			canvas.Draw(tex[pos.TexID], new Vector2(x, y), new Rectangle((int)(pos.Col * texColPixels), (int)(pos.Row * texRowPixels), (int)texColPixels, (int)texRowPixels), Color.White);
		}

		protected float[] MeasureString(string str)
		{
			float[] widths;
			widths = new float[str.Length];
			for (int i = 0; i < str.Length; i++)
			{
				widths[i] = _font.CharacterWidth(str.Substring(i, 1));
			}
			return widths;
		}
		public Texture2D DrawStringToTexture(string str)
		{
			System.Diagnostics.Trace.WriteLine("Dawn> Render String...Setting Target");
			float[] strWidth = MeasureString(str);
			
			GraphicsDevice graphicsDevice = DGE.Graphics.Device;
			RenderTarget2D rt = new RenderTarget2D(graphicsDevice, (int)strWidth.Sum(), (int)_font.MaxCharacterHeight());

			RenderTargetBinding[] old = graphicsDevice.GetRenderTargets();
			
			graphicsDevice.SetRenderTarget(rt);
			
			graphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 0, 0);
			System.Diagnostics.Trace.WriteLine("Dawn> Render String...Processing");
			SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
			spriteBatch.Begin();

			float x = 0;
			for (int i = 0; i < str.Length; i++)
			{
				System.Diagnostics.Trace.WriteLine("Dawn> Render String...Character #" + i.ToString());
				DrawCharacterToTexture((int)x, 0, str.Substring(i, 1), ref spriteBatch);
				x += strWidth[i];
			}

			spriteBatch.End();
			spriteBatch.Dispose();
			
			graphicsDevice.SetRenderTargets(old);
			

			graphicsDevice.Clear(Define.GameWindow.BackgroundColor());

			RenderTargetBinding binding=new RenderTargetBinding(rt);
			return binding.RenderTarget as Texture2D;
			
		}
	}
}
