using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic
{
	public class ExtendSprite : Sprite
	{
		public override string ObjectClassName() { return Define.EngineClassName.ExtendSprite(); }
		public float CenterX { get; set; }
		public float CenterY { get; set; }
		public float ScaleX { get; set; }
		public float ScaleY { get; set; }
		public float Rotation { get; set; }
		public float Scale
		{
			set
			{
				ScaleY = ScaleX = value;
			}
		}
		public Microsoft.Xna.Framework.Graphics.SpriteEffects Effect { get; set; }
		protected ExtendSprite()
			:base()
		{
		}

		public ExtendSprite(Dawn.Engine.Resource.Texture Tex, float x = 0, float y = 0, int z = 0, float centerX = 0, float centerY = 0, float rotation = 0, float scaleX = 1, float scaleY = 1)
			: base(Tex, x, y, z)
		{
			Effect = Microsoft.Xna.Framework.Graphics.SpriteEffects.None;
			CenterY = centerY;
			CenterX = centerX;
			Rotation = rotation;
			ScaleX = scaleX;
			ScaleY = scaleY;
		}
		public override void Update()
		{
			DGE.Graphics.Draw(tex, new Microsoft.Xna.Framework.Vector2(X, Y), new Microsoft.Xna.Framework.Rectangle(0, 0, tex.Width(), tex.Height()), color, Rotation, new Microsoft.Xna.Framework.Vector2(CenterX, CenterY), new Microsoft.Xna.Framework.Vector2(ScaleX, ScaleY), Effect, 0);
		}
	}
}
