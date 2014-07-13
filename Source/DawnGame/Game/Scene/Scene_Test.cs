using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dawn.Engine.Manager;
using Dawn;
using Dawn.Engine;
using System.Diagnostics;

namespace DawnGame.Game.Scene
{
	class Scene_Test : Dawn.Engine.Basic.Scene
	{
		Dawn.Engine.Manager.Processor.FontManager.FontHelper helper;
		Dawn.Engine.Resource.Audio audios;
		Dawn.Engine.Resource.Audio audio;

		string InputString;
		public Scene_Test()
		{
		}

		public override void Start()
		{
			base.Start();
			audios = new Dawn.Engine.Resource.Audio(DGE.Data.Audio("tmp.mp3"));
			audio = new Dawn.Engine.Resource.Audio(DGE.Data.Audio("3711.mp3"));
			Dawn.Engine.Resource.Font font = new Dawn.Engine.Resource.Font(new Dawn.Engine.Resource.Data.FontFamilyData(new System.Drawing.FontFamily("微软雅黑"), 22, System.Drawing.Color.White, false, false, false));
			
			
			
            audio.Load();
			//audios.Load();
			Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor processor=new Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor(audios);
			DGE.Threads.NewThread(processor);
			font.Load();
			helper = new Dawn.Engine.Manager.Processor.FontManager.FontHelper(font);
			/*
			Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor processor = new Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor(audios);
			System.Threading.Thread threadRes = new System.Threading.Thread(new System.Threading.ThreadStart(processor.Process));
			threadRes.IsBackground = true;
			
			threadRes.Start();
			while (threadRes.ThreadState != System.Threading.ThreadState.Stopped) ;
			*/
			//DGE.Audio.PlayBGM((Engine.Resource.Audio)processor.Res);
			
			//DGE.Audio.FadeOutStop(Engine.Define.EngineConst.AudioManager_ChannelType.BGM)
			//GC.Collect();
			DGE.Audio.PlayBGS(audio);
			while (!DGE.Threads.isEnd(processor)) ;
			DGE.Audio.FadeInPlay(Dawn.Engine.Define.EngineConst.AudioManager_ChannelType.BGM, audios);
			//DGE.Audio.PlayBGM(audios);
			InputString = "";
		}

		public override void Update()
		{
			helper.DrawString("あああああああ", 0, 0);
			helper.DrawString("FPS: " + DGE.Graphics.FPS, 0, 100);

			if(DGE.Input.MouseState.LeftButton==Microsoft.Xna.Framework.Input.ButtonState.Pressed)
			{
				DGE.Input.SetBusy(true);
			}
			else
			{
				DGE.Input.SetBusy(false);
			}
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
