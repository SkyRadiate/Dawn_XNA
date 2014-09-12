using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
	public class SpriteManager : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.SpriteManager(); }

		private List<Dawn.Engine.Basic.Sprite> SpriteSet;

		public SpriteManager()
		{
			SpriteSet = new List<Basic.Sprite>();
		}

		public void Initialize()
		{
		}
		public void Register(Dawn.Engine.Basic.Sprite spr)
		{
			int index = SpriteSet.IndexOf(spr);
			if (index == -1)
			{
				SpriteSet.Add(spr);
				SpriteSet.Sort(new Processor.SpriteManager.SpriteZComparer());
			}
		}
		public void UnRegister(Dawn.Engine.Basic.Sprite spr)
		{
			SpriteSet.Remove(spr);
		}
		public void ReRegister(Dawn.Engine.Basic.Sprite spr)
		{
			UnRegister(spr);
			Register(spr);
		}
		public void Update()
		{
			for (int i = SpriteSet.Count - 1; i >= 0; i--)
			{
				SpriteSet[i].Update();
			}
		}
	}
}
