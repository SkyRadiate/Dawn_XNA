using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Dawn.Engine.Manager;
using Dawn;
using Dawn.Engine;
using System.Diagnostics;
using Microsoft.Xna.Framework;

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
			supporter = new Dawn.Engine.Resource.Supporter.LyricSupporter(lrcFile, -20000);
			tex = new Microsoft.Xna.Framework.Graphics.Texture2D(DGE.Graphics.Device, 1, 1);
			tex1 = new Microsoft.Xna.Framework.Graphics.Texture2D(DGE.Graphics.Device, 1, 1);
			lstXY = new Vector2[1000000];
			lstAdd = new Vector2[1000000];
			used = new bool[1000000];

			randomer = new Random();
			lstLrc = "";

			alpha = 0;
		}

		string lstLrc;
		Microsoft.Xna.Framework.Graphics.Texture2D tex, tex1;
		bool[] used;
		Vector2[] lstAdd, lstXY;

		private int FindFree()
		{
			for (int i = 0; i < 1000000; i++)
			{
				if (used[i] == false)
				{
					return i;
				}
			}
			return -1;
		}

		private void Add(Vector2 vec)
		{
			int pos = FindFree();
			lstXY[pos] = vec;
			lstAdd[pos] = new Vector2(0, 0);
			used[pos] = true;
		}
		private void ProcessTex(Microsoft.Xna.Framework.Graphics.Texture2D tex, string str)
		{
			int offsetX = (int)((DGE.Graphics.Width() - helper.StringWidth(str)) / 2);
			int offsetY = (int)((DGE.Graphics.Height() - helper.StringHeight()) / 2);
			Microsoft.Xna.Framework.Color[] colorMap = new Microsoft.Xna.Framework.Color[tex.Width * tex.Height];
			tex.GetData<Microsoft.Xna.Framework.Color>(colorMap);
			for (int i = 0, k = 0; i < tex.Height; i++)
			{
				for (int j = 0; j < tex.Width; j++, k++)
				{
					if (colorMap[k].R != 0)
					{
						Add(new Microsoft.Xna.Framework.Vector2(j + offsetX, i + offsetY));
					}
				}
			}
		}
		Random randomer;
		int alpha;
		private void DrawTex()
		{
			Microsoft.Xna.Framework.Graphics.Texture2D tex=new Microsoft.Xna.Framework.Graphics.Texture2D(DGE.Graphics.Device,1,1);
			Color[] colorMap=new Color[1]{new Color(255,255,255)};
			tex.SetData<Color>(colorMap);
			for (int k = 0; k < 1000000; k++)
			{
				if (used[k])
				{
					DGE.Graphics.Canvas.Draw(tex, lstXY[k], Color.White);
					double c = Math.Sqrt(Math.Pow(lstXY[k].X - 512, 2) + Math.Pow(lstXY[k].Y - 384, 2));
					double a = 512 - lstXY[k].X;
					double b = 384 - lstXY[k].Y;

					double F,Fx,Fy;
					F=Fx=Fy=0;
					if (c != 0)
					{
						F = 1 / c;
						Fx = F / c * a;
						Fy = F / c * b;
					}
					//Fx /= 3; Fy /= 3;
					lstAdd[k].X += (float)Fx;
					lstAdd[k].Y += (float)Fy;
					lstXY[k].X += lstAdd[k].X;
					lstXY[k].Y += lstAdd[k].Y;


					if (lstXY[k].Y >= DGE.Graphics.Height()) used[k] = false;
				}
			}
		}
		public override void Update()
		{
			string lrc = supporter.GetLyric(watch.ElapsedMilliseconds);
			if (lrc != lstLrc)
			{
				tex = helper.DrawStringToTexture(lstLrc);
				ProcessTex(tex, lstLrc);

				lstLrc = lrc;

				
				alpha = 0;
				tex1 = helper.DrawStringToTexture(lrc);
			}
			Color nowColor = new Color(255, 255, 255) * (float)(alpha / 255.0f);
			DGE.Graphics.Canvas.Draw(tex1, new Vector2(((DGE.Graphics.Width() - helper.StringWidth(lrc)) / 2), ((DGE.Graphics.Height() - helper.StringHeight()) / 2)), nowColor);
			DrawTex();
			if (alpha < 255) alpha+=5;

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
