using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dawn.Engine.Manager;

namespace Dawn.Game.Scene
{
	class Scene_Test : Dawn.Engine.Basic.Scene
	{
		public Scene_Test()
		{
		}

		public override void Start()
		{
			base.Start();
            Engine.Resource.Audio audios = new Engine.Resource.Audio(DGE.Data.Audio("tmp.mp3"));

            Engine.Resource.Audio audio = new Engine.Resource.Audio(DGE.Data.Audio("3711.mp3"));
            audio.Load();
            DGE.Audio.PlayBGS(audio);
			Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor processor = new Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor(audios);
			System.Threading.Thread threadRes = new System.Threading.Thread(new System.Threading.ThreadStart(processor.Process));
			threadRes.IsBackground = true;
			
			threadRes.Start();
			while (threadRes.ThreadState != System.Threading.ThreadState.Stopped) ;
			//DGE.Audio.PlayBGM((Engine.Resource.Audio)processor.Res);
			DGE.Audio.FadeInPlay(Engine.Define.EngineConst.AudioManager_ChannelType.BGM, audios);
			//DGE.Audio.FadeOutStop(Engine.Define.EngineConst.AudioManager_ChannelType.BGM);
		}

		public override void Update()
		{
			DGE.Input.SetBusy(true);
			base.Update();
		}

		public override void End()
		{
			DGE.Audio.StopBGS();
			DGE.Audio.StopBGM();
			base.End();
		}
	}
}
