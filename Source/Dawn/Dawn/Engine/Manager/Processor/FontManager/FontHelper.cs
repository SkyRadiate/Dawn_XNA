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
	class FontHelper : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.FontHelper(); }

		protected Texture2D[] tex;
		protected string[,,] used;

		protected Resource.Font _font;

		protected bool[] texUsed;

		protected int texRow;
		protected int texCol;
		public FontHelper(Resource.Font font)
		{
			_font = font;

			texRow = EngineConst.FontHelper_TextureWidth() / _font.MaxCharacterWidth();
			texCol = EngineConst.FontHelper_TextureHeight() / _font.MaxCharacterHeight();
			texUsed = new bool[EngineConst.FontHelper_TextureNum()];
			used = new string[EngineConst.FontHelper_TextureNum(), texRow, texCol];

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

		protected virtual Helper.FontPosition _NewCharacter(string character)
		{
			return new Helper.FontPosition { TexID = 0, Row = 0, Col = 0 };
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
								_NewCharacter(character);
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
					for (int x = 0; x < texRow; x++)
					{
						for (int y = 0; y < texCol; y++)
						{
							if (used[i, x, y] == "")
							{
								_pos = new Helper.FontPosition { TexID = i, Row = x, Col = y };
								_NewCharacter(character);
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

		public Texture2D DrawStringToTexture(string str)
		{
			Texture2D tex = new Texture2D(DGE.Graphics.Device);
			return tex;
		}
	}
}
