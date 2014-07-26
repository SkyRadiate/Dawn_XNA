using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager.Processor.FontManager
{
	class FontDataComparer:IEqualityComparer<Engine.Resource.Data.FontFamilyData>
	{

		public bool Equals(Engine.Resource.Data.FontFamilyData dat1,Engine.Resource.Data.FontFamilyData dat2)
		{
			return dat1.Family.Name == dat2.Family.Name && dat1.Color == dat2.Color && dat1.isBlod == dat2.isBlod && dat1.Size == dat2.Size;
		}

		public int GetHashCode(Engine.Resource.Data.FontFamilyData dat)
		{
			int hCode = dat.Family.Name.GetHashCode() ^ dat.Color.R ^ dat.Color.G ^ dat.Color.B;
			return hCode.GetHashCode();
		}
	}
}
