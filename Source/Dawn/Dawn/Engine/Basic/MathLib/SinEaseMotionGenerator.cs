using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic.MathLib
{
	public class SinEaseMotionGenerator:MotionGenerator
	{
		public override double _GetStep(double Percent)
		{
			return (EndNumber - StartNumber) * (-Math.Cos(Percent * Math.PI) + 1) / 2 + StartNumber;
		}
	}
}
