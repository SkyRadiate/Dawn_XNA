using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic.MathLib
{
	public class CosShiftGenerator : MotionGenerator
	{
		public override double _GetStep(double Percent)
		{
			return (EndNumber - StartNumber) * (Math.Cos(Percent * Math.PI * 2) + 1) / 2 + StartNumber;
		}
	}
}
