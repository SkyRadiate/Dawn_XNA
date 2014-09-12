using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic.ThreadProcessor
{
	public class FPSProcessor : ThreadProcessor
	{
		private int Frames;
		private System.Timers.Timer timer;
		private System.Diagnostics.Stopwatch timeCalculator;
		public FPSProcessor()
			: base()
		{
			timeCalculator = new System.Diagnostics.Stopwatch();
			timer = new System.Timers.Timer(1000);
			timer.AutoReset = true;
			timer.Elapsed += new System.Timers.ElapsedEventHandler(Tick);
			timer.Start();
			timeCalculator.Start();
		}

		private void Tick(object sender, System.Timers.ElapsedEventArgs e)
		{
			FPS = Frames / timeCalculator.Elapsed.TotalSeconds;
			Frames = 0;
			timeCalculator.Restart();
			
		}

		public void AddFrame()
		{
			Frames++;
		}

		public double FPS { get; private set; }

		public override void Update()
		{

		}
	}
}
