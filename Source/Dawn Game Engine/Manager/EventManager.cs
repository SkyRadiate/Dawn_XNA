using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
	public delegate void SimpleEventHandler(object sender, EventArgs e);
	public delegate void MouseEventHandler(object sender, Processor.InputManager.MouseEventArgs e);
}
