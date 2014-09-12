using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Dawn.Engine.Manager.GraphicsManager")]
namespace Dawn.Engine.Basic
{
	class Bitmap : EngineObject
	{
		protected Dawn.Engine.Resource.Texture Tex;
		protected bool needCopy;
		protected byte[] colorMap;
		public override string ObjectClassName() { return Define.EngineClassName.Bitmap(); }

		public Bitmap(Dawn.Engine.Resource.Texture tex)
		{
			Tex = tex;
			needCopy = false;
		}

		internal void PreRender()
		{
			if(needCopy)
			{
				CopyToTexture();
				needCopy = false;
			}
		}
		private void CopyToTexture()
		{

		}

		private void CopyToByte()
		{

		}
	}
}
