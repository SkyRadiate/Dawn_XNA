using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager.Processor.AudioManager
{
	public class AudioProcessor_FadeIn : AudioProcessor
	{
		protected float nowVolume;
		public AudioProcessor_FadeIn(ref FMOD.Channel channel)
			: base(ref channel)
		{
			FMODRun(_Channel.getVolume(ref nowVolume));
		}
		public override void Update()
		{
			nowVolume += 1.0f / 300.0f;
			if (nowVolume > 1.0f)
			{
				_isEnd = true;
				return;
			}
			FMODRun(_Channel.setVolume(nowVolume));
		}
	}
}
