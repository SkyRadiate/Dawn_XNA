using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Basic.ThreadProcessor
{
	class FPSProcessor : ThreadProcessor
	{
		private int Frames;
		private System.Timers.Timer timer;
		public FPSProcessor()
			: base()
		{
			timer = new System.Timers.Timer(1);
			timer.AutoReset = true;
			timer.Elapsed += new System.Timers.ElapsedEventHandler(Tick);
			timer.Start();
		}

		private void Tick(object sender, System.Timers.ElapsedEventArgs e)
		{
			FPS = Frames;
			Frames = 0;
		}

		public void AddFrame()
		{
			Frames++;
		}

		public int FPS { get; private set; }

		public override void Update()
		{

		}
	}
}
