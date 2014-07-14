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
		Dawn.Engine.Resource.LyricFile lrcFile;
		Dawn.Engine.Resource.Supporter.LyricSupporter supporter;

		System.Diagnostics.Stopwatch watch;
		Microsoft.Xna.Framework.Graphics.Texture2D tmptex;
		double drawPosition;
		public Scene_Test()
		{
		}

		public override void Start()
		{
			base.Start();
			audios = new Dawn.Engine.Resource.Audio(DGE.Data.Audio("tmp.mp3"));
			audio = new Dawn.Engine.Resource.Audio(DGE.Data.Audio("3711.mp3"));
			Dawn.Engine.Resource.Font font = new Dawn.Engine.Resource.Font(new Dawn.Engine.Resource.Data.FontFamilyData(new System.Drawing.FontFamily("Segoe UI"), 22, System.Drawing.Color.White, false, false, false));



			audio.Load();
			//audios.Load();
			Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor processor = new Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor(audios);
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
			watch = new Stopwatch();
			watch.Start();
			//DGE.Audio.PlayBGM(audios);

			lrcFile = new Dawn.Engine.Resource.LyricFile(DGE.Data.LyricFile("一番の宝物.Jap.lrc"));
			lrcFile.Load();
			supporter = new Dawn.Engine.Resource.Supporter.LyricSupporter(lrcFile, -5000);


			tmptex = new Microsoft.Xna.Framework.Graphics.Texture2D(DGE.Graphics.Device, 1, 1);
			Microsoft.Xna.Framework.Color[] color = new Microsoft.Xna.Framework.Color[1] { new Microsoft.Xna.Framework.Color(255, 255, 255, 255) };
			tmptex.SetData<Microsoft.Xna.Framework.Color>(color);
			drawPosition = 0;
		}

		public override void Update()
		{
			helper.DrawString("あああああああ", 0, 0);
			helper.DrawString("FPS: " + DGE.Graphics.FPS, 0, 100);

			helper.DrawString(supporter.GetLyric(watch.ElapsedMilliseconds), 0, 150);
			//DGE.Graphics.Canvas.Draw(tmptex, new Microsoft.Xna.Framework.Vector2((int)drawPosition, (int)(DGE.Graphics.FPS / 5.0 + 100)), Microsoft.Xna.Framework.Color.White);
			drawPosition += 0.1;
			if (DGE.Input.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
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
