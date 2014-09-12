using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic.MathLib
{
	public class MotionGenerator : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.MotionGenerator(); }
		public double StartNumber { get; set; }
		public double EndNumber { get; set; }

		public virtual double _GetStep(double Percent)
		{
			return Percent * (EndNumber - StartNumber) + StartNumber;
		}

		public double GetStep(double Percent)
		{
			Percent = ((Percent % 1.0) + 1.0) % 1.0;
			return _GetStep(Percent);
		}
		public double GetStepWithFix(ref double Percent)
		{
			Percent = ((Percent % 1.0) + 1.0) % 1.0;
			return _GetStep(Percent);
		}
	}
}
