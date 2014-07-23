using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager.Processor.SpriteManager
{
	class SpriteZComparer : IComparer<Dawn.Engine.Basic.Sprite>
	{
		public int Compare(Dawn.Engine.Basic.Sprite spr1, Dawn.Engine.Basic.Sprite spr2)
		{
			return spr1.Z.CompareTo(spr2.Z);
		}
	}
}
