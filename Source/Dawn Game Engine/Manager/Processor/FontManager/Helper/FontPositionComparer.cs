using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager.Processor.FontManager.Helper
{
	class FontPositionComparer : IEqualityComparer<FontPosition>
	{

		public bool Equals(FontPosition pos1, FontPosition pos2)
		{
			return (pos1.TexID == pos2.TexID && pos1.Row == pos2.Row && pos1.Col == pos2.Col);
		}


		public int GetHashCode(FontPosition pos)
		{
			int hCode = pos.TexID ^ pos.Row ^ pos.Col;
			return hCode.GetHashCode();
		}

	}

}
