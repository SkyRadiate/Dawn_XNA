using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic
{
	public class Sprite : EngineObject, IDisposable
	{
		public override string ObjectClassName() { return Define.EngineClassName.Sprite(); }

		public float X { get; set; }
		public float Y { get; set; }
		public float Width { get { return tex.Width(); } }
		public float Height { get { return tex.Height(); } }
		private int _Z;
		public int Z
		{
			get
			{
				return _Z;
			}
			set
			{
				_Z = value;
				DGE.Graphics.Sprites.ReRegister(this);
			}
		}
		public Microsoft.Xna.Framework.Color color
		{
			get
			{
				return (new Microsoft.Xna.Framework.Color(ColorR, ColorG, ColorB)) * (ColorA / 255.0f);
			}
			set
			{
				ColorR = value.R;
				ColorG = value.G;
				ColorB = value.B;
				ColorA = value.A;
			}
		}
		public float ColorR { get; set; }
		public float ColorG { get; set; }
		public float ColorB { get; set; }
		public float ColorA { get; set; }
		public Dawn.Engine.Resource.Texture tex { get; set; }

		protected Sprite()
		{
			DGE.Graphics.Sprites.Register(this);
			color = Microsoft.Xna.Framework.Color.White;
		}

		public Sprite(Dawn.Engine.Resource.Texture Tex, float x = 0, float y = 0, int z = 0)
			: this()
		{
			this.tex = Tex;
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public void Dispose()
		{
			if (DGE.Graphics != null)
			{
				if (DGE.Graphics.Sprites != null)
				{
					DGE.Graphics.Sprites.UnRegister(this);
				}
			}
		}

		public virtual void Update()
		{
			DGE.Graphics.Draw(tex, new Microsoft.Xna.Framework.Vector2(X, Y), color);
		}
	}
}
