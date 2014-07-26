using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic
{
	class UIObject : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.UIObject(); }
		public Dawn.Engine.Basic.GraphicsLib.GraphicsArea AreaChecker { get; set; }
		public Dawn.Engine.Basic.Sprite Sprite { get; set; }

		public void RefreshArea()
		{
			AreaChecker = new GraphicsLib.GraphicsAreaRectangle(DGE.Graphics.Width(), DGE.Graphics.Height())
			{
				RectangleArea = new Microsoft.Xna.Framework.Rectangle((int)Sprite.X, (int)Sprite.Y, (int)Sprite.Width, (int)Sprite.Height)
			};
		}

		public int Z { get { return Sprite.Z; } set { Sprite.Z = value; } }
		public virtual void Update()
		{

		}
	}
}
