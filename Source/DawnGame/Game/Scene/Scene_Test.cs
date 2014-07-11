using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dawn.Engine.Manager;
using Dawn;
using Dawn.Engine;

namespace DawnGame.Game.Scene
{
	class Scene_Test : Dawn.Engine.Basic.Scene
	{
		public Scene_Test()
		{
		}

		public override void Start()
		{
			base.Start();
			Dawn.Engine.Resource.Audio audios = new Dawn.Engine.Resource.Audio(DGE.Data.Audio("tmp.mp3"));

			Dawn.Engine.Resource.Audio audio = new Dawn.Engine.Resource.Audio(DGE.Data.Audio("3711.mp3"));
            audio.Load();
            DGE.Audio.PlayBGS(audio);
			Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor processor = new Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor(audios);
			System.Threading.Thread threadRes = new System.Threading.Thread(new System.Threading.ThreadStart(processor.Process));
			threadRes.IsBackground = true;
			
			threadRes.Start();
			while (threadRes.ThreadState != System.Threading.ThreadState.Stopped) ;
			//DGE.Audio.PlayBGM((Engine.Resource.Audio)processor.Res);
			DGE.Audio.FadeInPlay(Dawn.Engine.Define.EngineConst.AudioManager_ChannelType.BGM, audios);
			//DGE.Audio.FadeOutStop(Engine.Define.EngineConst.AudioManager_ChannelType.BGM)

			Dawn.Engine.Resource.Font font = new Dawn.Engine.Resource.Font(new Dawn.Engine.Resource.Data.FontFamilyData(new System.Drawing.FontFamily("微软雅黑"), 28, false, false, false));
			font.Load();
			Dawn.Engine.Manager.Processor.FontManager.FontHelper helper = new Dawn.Engine.Manager.Processor.FontManager.FontHelper(font);
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
