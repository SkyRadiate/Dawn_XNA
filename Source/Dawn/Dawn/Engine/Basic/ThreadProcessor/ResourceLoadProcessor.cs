using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic.ThreadProcessor
{
	public class ResourceLoadProcessor : ThreadProcessor
	{
		protected Resource.Resource res;

		public ResourceLoadProcessor(Resource.Resource resource)
			: base()
		{
			res = resource;
		}

		public Resource.Resource Res { get { return res; } }

		public override void Update()
		{
			lock (res)
			{
				res.Load();
			}
			EndUpdate();
		}
	}
}
