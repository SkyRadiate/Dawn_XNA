using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic.MathLib
{
	public class SinEaseinMotionGenerator : MotionGenerator
	{
		public override double _GetStep(double Percent)
		{
			return (EndNumber - StartNumber) * (-Math.Cos(Percent * Math.PI / 2) + 1) + StartNumber;
		}
	}
}
