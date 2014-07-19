using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
	public class SpriteManager : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.SpriteManager(); }

		private SortedSet<Dawn.Engine.Basic.Sprite> spriteSet;

		public SpriteManager()
		{
			spriteSet = new SortedSet<Basic.Sprite>();
		}

		public void Initialize()
		{
			DGE.Graphics.PostRender += Graphics_PostRender;
		}

		void Graphics_PostRender(object Object, EventArgs e)
		{

		}

		public void Register(Dawn.Engine.Basic.Sprite spr)
		{
			spriteSet.Add(spr);
		}

		public void Update()
		{

		}
	}
}
