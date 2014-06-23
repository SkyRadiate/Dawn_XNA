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
            Engine.Resource.AudioStream audios = new Engine.Resource.AudioStream(DGE.Data.Audio("tmp.mp3"));

            Engine.Resource.Audio audio = new Engine.Resource.Audio(DGE.Data.Audio("3711.mp3"));
            audio.Load();
            audios.Load();
            DGE.Audio.PlayBGS(audio);
            //DGE.Audio.PlayBGM(audios);
			DGE.Audio.Play(Engine.Define.EngineConst.AudioManager_ChannelType.BGM, audios);

			DGE.Audio.FadeOutStop(Engine.Define.EngineConst.AudioManager_ChannelType.BGM);
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
