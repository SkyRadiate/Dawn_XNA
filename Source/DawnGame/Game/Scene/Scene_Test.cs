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
		public Scene_Test()
		{
		}

		public override void Start()
		{
			base.Start();
			audios = DGE.Cache.Audio(DGE.Data.Audio("tmp.mp3"));
			audio = DGE.Cache.Audio(DGE.Data.Audio("3711.mp3"));
			Dawn.Engine.Resource.Font font = DGE.Cache.Font(new Dawn.Engine.Resource.Data.FontFamilyData(new System.Drawing.FontFamily("Segoe UI"), 32, System.Drawing.Color.White, false, false, false));

			Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor processor = new Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor(audios);
			DGE.Threads.NewThread(processor);
			helper = new Dawn.Engine.Manager.Processor.FontManager.FontHelper(font);

			DGE.Audio.PlayBGS(audio);
			DGE.Audio.FadeInPlay(Dawn.Engine.Define.EngineConst.AudioManager_ChannelType.BGM, audios);

			watch = new Stopwatch();
			watch.Start();

			lrcFile = DGE.Cache.LyricFile(DGE.Data.LyricFile("一番の宝物.Jap.lrc"));
			supporter = new Dawn.Engine.Resource.Supporter.LyricSupporter(lrcFile, 0);

		}

		public override void Update()
		{
			helper.DrawString("あああああああ", 0, 0);
			helper.DrawString("FPS: " + DGE.Graphics.FPS, 0, 100);

			helper.DrawString(supporter.GetLyric(watch.ElapsedMilliseconds), 0, 150);

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
