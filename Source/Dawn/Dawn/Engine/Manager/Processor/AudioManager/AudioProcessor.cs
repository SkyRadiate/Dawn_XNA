using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager.Processor.AudioManager
{
	public class AudioProcessor : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.AudioProcessor(); }

		protected FMOD.Channel _Channel;
		protected bool _isEnd;
		public AudioProcessor(ref FMOD.Channel channel)
		{
			_Channel = channel;
			_isEnd = false;
		}

		public virtual void Update()
		{
			_isEnd = true;
		}

		public bool isEnd()
		{
			return _isEnd;
		}

		public void FMODRun(FMOD.RESULT result)
		{
			if (result != FMOD.RESULT.OK)
			{
				DGE.Debug.Error(this, Define.EngineErrorName.AudioManager_FMODError(), Define.EngineErrorDetail.FMODError() + Define.EngineErrorDetail.Separator() + FMOD.Error.String(result));
			}
		}
	}
}
