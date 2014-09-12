using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager.Processor.AudioManager
{
	public class AudioProcessor_FadeOut : AudioProcessor
	{
		protected float nowVolume;
		public AudioProcessor_FadeOut(ref FMOD.Channel channel)
			: base(ref channel)
		{
			FMODRun(_Channel.getVolume(ref nowVolume));
		}
		public override void Update()
		{
			nowVolume -= 1.0f / 300.0f;
			if (nowVolume < 0.0f)
			{
				_isEnd = true;
				FMODRun(_Channel.stop());
				return;
			}
			FMODRun(_Channel.setVolume(nowVolume));
		}
	}
}
