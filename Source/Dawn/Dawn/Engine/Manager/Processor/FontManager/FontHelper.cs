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
		}

		protected virtual void _DrawCharacter(string character,ref SpriteBatch canvas,Vector2 position)
		{
			canvas.Draw(DGE.Input.mouseTexture.GetTexture(), position, Color.White);
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
			_DrawCharacter(character, ref spriteBatch, new Vector2(position.Col * texColPixels, position.Row * texRowPixels));

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
			float[] strWidth = MeasureString(str);
			
			GraphicsDevice graphicsDevice = DGE.Graphics.Device;
			RenderTarget2D rt = new RenderTarget2D(graphicsDevice, (int)strWidth.Sum(), (int)_font.MaxCharacterHeight());

			RenderTargetBinding[] old = graphicsDevice.GetRenderTargets();
			
			graphicsDevice.SetRenderTarget(rt);
			
			graphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 0, 0);
			
			SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
			spriteBatch.Begin();

			float x = 0;
			for (int i = 0; i < str.Length; i++)
			{
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
