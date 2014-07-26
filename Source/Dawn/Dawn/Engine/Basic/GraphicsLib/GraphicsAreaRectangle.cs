using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic.GraphicsLib
{
	public class GraphicsAreaRectangle : GraphicsArea
	{
		public GraphicsAreaRectangle(int CanvasWidth, int CanvasHeight)
			: base(CanvasWidth, CanvasHeight)
		{
			Width = CanvasWidth; Height = CanvasHeight;
		}

		public Microsoft.Xna.Framework.Rectangle RectangleArea { get; set; }

		protected override void _Resize(int newWidth, int newHeight)
		{
			int tmpWidth = RectangleArea.Width, tmpHeight = RectangleArea.Height;
			if (tmpWidth > newWidth) tmpWidth = newWidth;
			if (tmpHeight > newHeight) tmpHeight = newHeight;
			RectangleArea = new Microsoft.Xna.Framework.Rectangle(RectangleArea.X, RectangleArea.Y, tmpWidth, tmpHeight);
			return;
		}

		protected virtual bool isInArea(int x, int y)
		{
			if (RectangleArea.Left <= x && x <= RectangleArea.Right)
			{
				if (RectangleArea.Top <= y && y <= RectangleArea.Bottom)
				{
					return true;
				}
			}
			return false;
		}

		public override object Clone()
		{
			GraphicsAreaRectangle graphicsArea = new GraphicsAreaRectangle(Width, Height) { RectangleArea = this.RectangleArea };
			return graphicsArea;
		}
	}
}
