using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic.GraphicsLib
{
	public class GraphicsArea : EngineObject, ICloneable
	{
		public override string ObjectClassName() { return Define.EngineClassName.GraphicsArea(); }

		protected int Width, Height;

		public void Resize(int width, int height)
		{
			_Resize(width, height);
			Width = width; Height = height;
		}
		public GraphicsArea(int CanvasWidth, int CanvasHeight)
		{
			Width = CanvasWidth; Height = CanvasHeight;
		}

		protected virtual void _Resize(int newWidth, int newHeight)
		{

		}

		protected virtual bool isInArea(int x, int y)
		{
			return true;
		}

		public virtual object Clone()
		{
			return new GraphicsArea(Width, Height);
		}
	}

}
