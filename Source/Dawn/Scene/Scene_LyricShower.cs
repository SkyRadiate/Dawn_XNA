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

namespace Dawn.Game.Scene
{
	class Scene_LyricShower : Dawn.Engine.Basic.Scene
	{
		Dawn.Engine.Manager.Processor.FontManager.FontHelper helper;
		Dawn.Engine.Resource.Audio audios;
		Dawn.Engine.Resource.LyricFile lrcFile;
		Dawn.Engine.Resource.Supporter.LyricSupporter supporter;

		System.Diagnostics.Stopwatch watch;
		Microsoft.Xna.Framework.Graphics.Texture2D texTMP;
		private const int MAX_POINT = 10000000;
		public Scene_LyricShower()
		{
		}
		
		public override void Start()
		{
			base.Start();
			audios = DGE.Cache.AudioStream(DGE.Data.Audio("tmp.mp3"));

			Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor processor = new Dawn.Engine.Basic.ThreadProcessor.ResourceLoadProcessor(audios);
			DGE.Threads.NewThread(processor);
			helper = new Dawn.Engine.Manager.Processor.FontManager.FontHelper(DGE.Data.Cache.Font(new Dawn.Engine.Resource.Data.FontFamilyData(new System.Drawing.FontFamily("微软雅黑"), 32, System.Drawing.Color.White, false)));

			DGE.Audio.FadeInPlay(Dawn.Engine.Define.EngineConst.AudioManager_ChannelType.BGM, audios);

			watch = new Stopwatch();
			watch.Start();

			lrcFile = DGE.Cache.LyricFile(DGE.Data.LyricFile("一番の宝物.Jap.lrc"));
			supporter = new Dawn.Engine.Resource.Supporter.LyricSupporter(lrcFile, -1000);
			tex = Dawn.Engine.Resource.Texture.CreateWithoutCache(1, 1);
			tex1 = Dawn.Engine.Resource.Texture.CreateWithoutCache(1, 1);
			lstXY = new Vector2[MAX_POINT];
			lstAdd = new Vector2[MAX_POINT];
			used = new bool[MAX_POINT];

			randomer = new Random();
			lstLrc = "";

			alpha = 0;
			texTMP = new Microsoft.Xna.Framework.Graphics.Texture2D(DGE.Graphics.Device, 2, 2);
			Color[] colorMap = new Color[4] { new Color(255, 255, 255), new Color(255, 255, 255), new Color(255, 255, 255), new Color(255, 255, 255) };
			texTMP.SetData<Color>(colorMap);
		}

		string lstLrc;
		Dawn.Engine.Resource.Texture tex, tex1;
		bool[] used;
		Vector2[] lstAdd, lstXY;

		private int FindFree()
		{
			for (int i = 0; i < MAX_POINT; i++)
			{
				if (used[i] == false)
				{
					return i;
				}
			}
			return -1;
		}

		private double RandomDouble()
		{
			const int MAX_NUMBER=1000000;
			const int MAX_CUT=80000;
			return (randomer.Next(0, MAX_NUMBER) - MAX_NUMBER / 2) / MAX_CUT;
		}

		private void Add(Vector2 vec)
		{
			int pos = FindFree();
			lstXY[pos] = new Vector2(vec.X, vec.Y + 50);
			lstAdd[pos] = new Vector2((float)RandomDouble(), (float)RandomDouble());
			used[pos] = true;
		}
		private void ProcessTex(Dawn.Engine.Resource.Texture texR, string str)
		{
			int offsetX = (int)((DGE.Graphics.Width() - helper.StringWidth(str)) / 2);
			int offsetY = (int)((DGE.Graphics.Height() - helper.StringHeight()) / 2);
			Microsoft.Xna.Framework.Graphics.Texture2D tex = texR.GetTexture();
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
		float alpha;
		private void DrawTex()
		{

			//Color tmpColor = new Color(51, 204, 255);
			Color tmpColor = new Color(200, 200, 200);
			for (int k = 0; k < MAX_POINT; k++)
			{
				if (used[k])
				{
					DGE.Graphics.Canvas.Draw(texTMP, lstXY[k], tmpColor);
					double c = Math.Sqrt(Math.Pow(lstXY[k].X - DGE.Graphics.Width() / 2, 2) + Math.Pow(lstXY[k].Y - DGE.Graphics.Height() / 2, 2));
					double a = DGE.Graphics.Width() / 2 - lstXY[k].X - lstAdd[k].X;
					double b = DGE.Graphics.Height() / 2 - lstXY[k].Y - lstAdd[k].Y;

					double F, Fx, Fy;
					F = Fx = Fy = 0;
					if (c != 0)
					{
						F = (1280 - c) / 1280;
						Fx = F / c * a;
						Fy = F / c * b;
					}
					Fx /= 3; Fy /= 3;
					lstAdd[k].X += (float)Fx;
					lstAdd[k].Y += (float)Fy;
					lstXY[k].X += lstAdd[k].X;
					lstXY[k].Y += lstAdd[k].Y;


					if (lstXY[k].Y >= DGE.Graphics.Height()) used[k] = false;
					if (c <= 1)
					{
						used[k] = false;
					}
				}
			}
		}
		public override void Update()
		{
			base.Update();

			string lrc = supporter.GetLyric(watch.ElapsedMilliseconds);
			if (lrc != lstLrc)
			{
				tex = helper.DrawStringToTexture(lstLrc);
				ProcessTex(tex, lstLrc);

				lstLrc = lrc;

				tex1 = helper.DrawStringToTexture(lrc);
				alpha = 0;
			} 
			Color nowColor = new Color(255, 255, 255) *(float)(alpha / 255.0f);
			DrawTex();
			DGE.Graphics.Draw(tex1, new Vector2(((DGE.Graphics.Width() - helper.StringWidth(lrc)) / 2), ((DGE.Graphics.Height() - helper.StringHeight()) / 2) + (255 - alpha) / 3 + 50), nowColor);

			alpha += (float)(255f / 60f);
			if (alpha > 255)
			{
				alpha = 255;
			}
		}

		public override void End()
		{
			DGE.Audio.StopBGS();
			DGE.Audio.StopBGM();
			base.End();
		}
	}
}
