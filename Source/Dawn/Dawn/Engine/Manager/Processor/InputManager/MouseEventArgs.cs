using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Dawn.Engine.Manager.InputManager")]
namespace Dawn.Engine.Manager.Processor.InputManager
{
	public enum MouseButtonStatus
	{ 
		Pressed=0,
		Released
	};
	public class MouseEventArgs : EngineObject,ICloneable
	{
		public override string ObjectClassName() { return Dawn.Engine.Define.EngineClassName.MouseEventArgs(); }
		public MouseButtonStatus ButtonLeft { get; internal set; }
		public MouseButtonStatus ButtonRight { get; internal set; }
		public MouseButtonStatus ButtonMiddle { get; internal set; }
		public int X { get; internal set; }
		public int Y { get; internal set; }
		public object Clone()
		{
			return new MouseEventArgs
			{
				ButtonLeft = this.ButtonLeft,
				ButtonRight = this.ButtonRight,
				ButtonMiddle = this.ButtonMiddle,
				X = this.X,
				Y = this.Y
			};
		}
	}
}
