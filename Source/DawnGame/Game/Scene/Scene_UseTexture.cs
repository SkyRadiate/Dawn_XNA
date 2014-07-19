using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dawn;

namespace DawnGame.Game.Scene
{
	class Scene_UseTexture : Dawn.Engine.Basic.Scene
	{
		Dawn.Engine.Resource.Texture tex;
		Microsoft.Xna.Framework.Vector2[] pos;
		Microsoft.Xna.Framework.Vector2[] posAdd;
		Microsoft.Xna.Framework.Color[] colors;
		Dawn.Engine.Manager.Processor.FontManager.FontHelper helper;

		float[] rotate, rotateDest;
		float[] scale, scaleDest;
		const int MAX_SPRITE_NUM = 4000;
		Random randomer;
		public Scene_UseTexture()
		{
			
		}
		
		public override void Start()
		{
			base.Start();
			tex = DGE.Data.Cache.Graphics(DGE.Data.SystemTexture("tex_zazaka"));
			pos = new Microsoft.Xna.Framework.Vector2[MAX_SPRITE_NUM];
			posAdd = new Microsoft.Xna.Framework.Vector2[MAX_SPRITE_NUM];
			colors = new Microsoft.Xna.Framework.Color[MAX_SPRITE_NUM];
			rotate = new float[MAX_SPRITE_NUM];
			rotateDest = new float[MAX_SPRITE_NUM];
			scale = new float[MAX_SPRITE_NUM];
			scaleDest = new float[MAX_SPRITE_NUM]; 
			randomer = new Random();

			helper = new Dawn.Engine.Manager.Processor.FontManager.FontHelper(DGE.Data.Cache.Font(new Dawn.Engine.Resource.Data.FontFamilyData(new System.Drawing.FontFamily("微软雅黑"), 32, System.Drawing.Color.White, false, false, false)));
			InitPos();
		}

		private void InitPos()
		{
			for (int i = 0; i < MAX_SPRITE_NUM; i++)
			{
				pos[i] = new Microsoft.Xna.Framework.Vector2(randomer.Next(0, DGE.Graphics.Width()), randomer.Next(0, DGE.Graphics.Height()));
				posAdd[i] = new Microsoft.Xna.Framework.Vector2(randomer.Next(-5, 5), randomer.Next(-5, 5));
				colors[i] = new Microsoft.Xna.Framework.Color(randomer.Next(0, 255), randomer.Next(0, 255), randomer.Next(0, 255));
				rotate[i] = randomer.Next(0, 359);
				rotateDest[i] = randomer.Next(0, 369);
				scale[i] = (float)randomer.Next(100, 200) / 100;
				scaleDest[i] = (float)randomer.Next(100, 200) / 100;
			}
		}
		private void ProcessPos()
		{
			for (int i = 0; i < MAX_SPRITE_NUM; i++)
			{
				pos[i].X += posAdd[i].X;
				pos[i].Y += posAdd[i].Y;
				if (pos[i].X < 0) posAdd[i].X = Math.Abs(posAdd[i].X);
				if (pos[i].Y < 0) posAdd[i].Y = Math.Abs(posAdd[i].Y);
				if (pos[i].X >= DGE.Graphics.Width()) posAdd[i].X = -Math.Abs(posAdd[i].X);
				if (pos[i].Y >= DGE.Graphics.Height()) posAdd[i].Y = -Math.Abs(posAdd[i].Y);

				if (rotate[i] < rotateDest[i]) rotate[i] += 0.1f;
				if (rotate[i] > rotateDest[i]) rotate[i] -= 0.1f;
				if (rotate[i] == rotateDest[i]) rotateDest[i] = randomer.Next(0, 369);

				if (scale[i] < scaleDest[i]) scale[i] += 0.03f;
				if (scale[i] > scaleDest[i]) scale[i] -= 0.03f;
				if (Math.Abs(scale[i] - scaleDest[i]) <= 0.03) scaleDest[i] = (float)randomer.Next(100, 200) / 100;

			}
		}
		public override void Update()
		{
			base.Update();
			for (int i = 0; i < MAX_SPRITE_NUM; i++)
			{
				Microsoft.Xna.Framework.Vector2 position = pos[i];
				position.X -= tex.Width() / 2;
				position.Y -= tex.Height() / 2;
				DGE.Graphics.Draw(tex, position, colors[i], rotate[i], (float)scale[i]);
			}
			ProcessPos();
			helper.DrawString(DGE.Graphics.FPS.ToString(), 10, 10);
		}

		public override void End()
		{
			base.End();
		}
	}
}
