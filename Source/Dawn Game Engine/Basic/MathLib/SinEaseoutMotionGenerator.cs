using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic.MathLib
{
	public class SinEaseoutMotionGenerator : MotionGenerator
	{
		public override double _GetStep(double Percent)
		{
			return (EndNumber - StartNumber) * Math.Sin(Percent * Math.PI / 2) + StartNumber;
		}
	}
}
