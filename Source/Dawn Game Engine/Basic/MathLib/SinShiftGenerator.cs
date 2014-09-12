using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic.MathLib
{
	public class SinShiftGenerator : MotionGenerator
	{
		public override double _GetStep(double Percent)
		{
			return (EndNumber - StartNumber) * (Math.Sin(Percent * Math.PI * 2) + 1) / 2 + StartNumber;
		}
	}
}
