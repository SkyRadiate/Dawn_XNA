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
			managedTexs = new List<Microsoft.Xna.Framework.Graphics.Texture2D>();
			DGE.Graphics.WhenDeviceChanging += new EventHandler<EventArgs>(Graphics_WhenDeviceChanging);
			DGE.Graphics.WhenDeviceChanged += new EventHandler<EventArgs>(Graphics_WhenDeviceChanged);
		}

		void Graphics_WhenDeviceChanged(object sender, EventArgs e)
		{
			managedTexs.ForEach(delegate(Microsoft.Xna.Framework.Graphics.Texture2D tex)
			{
				if (tex == null)
				{
					DGE.Debug.Warning(this, Define.EngineErrorName.TextureManager_NullManagedTexture(), Define.EngineErrorDetail.Empty());
				}
				else
				{
					GetTexture(tex);
				}
			});
		}

		void Graphics_WhenDeviceChanging(object sender, EventArgs e)
		{
			managedTexs.ForEach(delegate(Microsoft.Xna.Framework.Graphics.Texture2D tex)
			{
				if (tex == null)
				{
					DGE.Debug.Warning(this, Define.EngineErrorName.TextureManager_NullManagedTexture(), Define.EngineErrorDetail.Empty());
				}
				else
				{
					SaveTexture(tex);
				}
			});
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
			if(pos==-1)
			{
				DGE.Debug.Error(this, Define.EngineErrorName.TextureManager_TooManyTexture(), Define.EngineErrorDetail.Empty());
			}
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
			GC.Collect();
		}


		private List<Microsoft.Xna.Framework.Graphics.Texture2D> managedTexs;
		public void ManageTexture(Microsoft.Xna.Framework.Graphics.Texture2D tex)
		{
			if (managedTexs.IndexOf(tex) == -1)
			{
				managedTexs.Add(tex);
			}
			else
			{
				DGE.Debug.Warning(this, Define.EngineErrorName.TextureManager_RepeatTexture(), Define.EngineErrorDetail.Empty());
			}
		}

		public void UnmanageTexture(Microsoft.Xna.Framework.Graphics.Texture2D tex)
		{
			if (managedTexs.IndexOf(tex) != -1)
			{
				managedTexs.Remove(tex);
			}
			else
			{
				DGE.Debug.Warning(this, Define.EngineErrorName.TextureManager_RemoveWithoutAdd(), Define.EngineErrorDetail.Empty());
			} 
		}
	}
}
