using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
	public class TextureManager : EngineObject
	{
		byte[][] lst;
		bool[] usedLst;
		Dictionary<Microsoft.Xna.Framework.Graphics.Texture2D, int> dict;
		public override string ObjectClassName() { return Define.EngineClassName.TextureManager(); }

		public TextureManager()
		{
			dict = new Dictionary<Microsoft.Xna.Framework.Graphics.Texture2D, int>();
			lst = new byte[Define.EngineConst.TextureManager_MaxTextureNumber()][];
			usedLst = new bool[Define.EngineConst.TextureManager_MaxTextureNumber()];
			for(int i=0;i<Define.EngineConst.TextureManager_MaxTextureNumber();i++)
			{
				usedLst[i] = false;
			}
		}
		public void Initialize()
		{

		}
		private int FindFree()
		{
			int pos;
			for (pos = 0; pos < Define.EngineConst.TextureManager_MaxTextureNumber(); pos++)
			{
				if (usedLst[pos]==false)
				{
					return pos;
				}
			}
			return -1;
		}
		public void SaveTexture(Microsoft.Xna.Framework.Graphics.Texture2D tex)
		{
			int pos = FindFree();
			dict.Add(tex, pos);
			byte[] tmp = new byte[tex.Width * tex.Height * 4];
			tex.GetData<byte>(tmp);
			lst[pos] = tmp;
			usedLst[pos] = true;
		}

		public void GetTexture(Microsoft.Xna.Framework.Graphics.Texture2D tex)
		{
			int pos;
			dict.TryGetValue(tex, out pos);
			dict.Remove(tex);
			tex.SetData<byte>(lst[pos]);
			usedLst[pos] = false;
			lst[pos] = null;
		}

	}
}
