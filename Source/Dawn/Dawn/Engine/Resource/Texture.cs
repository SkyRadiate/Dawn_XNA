using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Dawn.Engine.Resource
{
	public class Texture : Resource
	{
        public override string ObjectClassName() { return Define.EngineClassName.TextureResource(); }

		Texture2D tex;
        public Texture()
            : base()
        {
        }

        public Texture(string filename)
            : base(filename)
        {
        }
        public override void Load()
        {
            base.Load();
			tex = DGE.Data.Content.Load<Texture2D>(_filename);
        }
        public override void Unload()
        {
			tex.Dispose();
            base.Unload();
        }
        public Texture2D GetTexture()
        {
            if (isLoad())
            {
				return tex;
            }
            else
            {
                DGE.Debug.Error(this, Define.EngineErrorName.Resource_CannotGetResource(), GetErrorDetail());
                return null;
            }
        }

		public int Width()
		{
			if (isLoad())
			{
				return tex.Width;
			}
			else
			{
				DGE.Debug.Error(this, Define.EngineErrorName.Resource_CannotGetResource(), GetErrorDetail());
				return -1;
			}
		}
		public int Height()
		{
			if (isLoad())
			{
				return tex.Height;
			}
			else
			{
				DGE.Debug.Error(this, Define.EngineErrorName.Resource_CannotGetResource(), GetErrorDetail());
				return -1;
			}
		}
		public static Texture CreateTexture(int width, int height)
		{
			Texture tex = CreateWithoutCache(width, height);
			DGE.TextureCache.ManageTexture(tex.tex);
			return tex;
		}
		public static Texture CreateTexture(Texture2D tex)
		{
			Texture texR = CreateWithoutCache(tex);
			DGE.TextureCache.ManageTexture(texR.tex);
			return texR;
		}
		public static Texture CreateWithoutCache(Texture2D tex)
		{
			Texture texR = new Texture();
			texR._isLoad = true;
			texR.tex = tex;
			return texR;
		}
		public static Texture CreateWithoutCache(int width, int height)
		{
			Texture tex = new Texture();
			tex._isLoad = true;
			tex.tex = new Texture2D(DGE.Graphics.Device, width, height);
			return tex;
		}

		public override object Clone()
		{
			Texture res = new Texture();
			res._isLoad = true;
			byte[] colorMap = new byte[tex.Width * tex.Height * 4];
			tex.GetData<byte>(colorMap);
			res.tex = new Texture2D(DGE.Graphics.Device, tex.Width, tex.Height);
			res.tex.SetData<byte>(colorMap);
			DGE.TextureCache.ManageTexture(res.tex);
			return res;
		}
	}
}
