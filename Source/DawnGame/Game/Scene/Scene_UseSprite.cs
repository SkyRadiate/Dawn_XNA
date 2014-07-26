using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dawn;

namespace DawnGame.Game.Scene
{
	class Scene_UseSprite : Dawn.Engine.Basic.Scene
	{
		Dawn.Engine.Basic.ExtendSprite[] spr;
		Dawn.Engine.Basic.MathLib.MotionGenerator[] motion;
		Dawn.Engine.Basic.MathLib.MotionGenerator[] shift;
		int[] shiftRate;
		double Percent;

		Dawn.Engine.Manager.Processor.FontManager.FontHelper font;
		public Scene_UseSprite()
		{

		}
		public override void Start()
		{
			base.Start();
			spr = new Dawn.Engine.Basic.ExtendSprite[4];
			Dawn.Engine.Resource.Texture tex=DGE.Data.Cache.Graphics(DGE.Data.SystemTexture("tex_zazaka"));
			spr[0] = new Dawn.Engine.Basic.ExtendSprite(tex.Clone() as Dawn.Engine.Resource.Texture, y: 0f, z: 4);
			spr[1] = new Dawn.Engine.Basic.ExtendSprite(tex.Clone() as Dawn.Engine.Resource.Texture, y: 100f, z: 3);
			spr[2] = new Dawn.Engine.Basic.ExtendSprite(tex.Clone() as Dawn.Engine.Resource.Texture, y: 200f, z: 2);
			spr[3] = new Dawn.Engine.Basic.ExtendSprite(tex.Clone() as Dawn.Engine.Resource.Texture, y: 300f, z: 1);
			motion = new Dawn.Engine.Basic.MathLib.MotionGenerator[4];
			motion[0] = new Dawn.Engine.Basic.MathLib.SinEaseMotionGenerator { StartNumber = 0, EndNumber = DGE.Graphics.Width() - spr[0].tex.Width() };
			motion[1] = new Dawn.Engine.Basic.MathLib.SinEaseinMotionGenerator { StartNumber = 0, EndNumber = DGE.Graphics.Width() - spr[1].tex.Width() };
			motion[2] = new Dawn.Engine.Basic.MathLib.SinEaseoutMotionGenerator { StartNumber = 0, EndNumber = DGE.Graphics.Width() - spr[2].tex.Width() };
			motion[3] = new Dawn.Engine.Basic.MathLib.MotionGenerator { StartNumber = 0, EndNumber = DGE.Graphics.Width() - spr[3].tex.Width() };
			shift = new Dawn.Engine.Basic.MathLib.MotionGenerator[4];
			shiftRate = new int[4];
			int YPosition = 35;
			shift[0] = new Dawn.Engine.Basic.MathLib.SinShiftGenerator { StartNumber = YPosition, EndNumber = YPosition += 50 };
			shift[1] = new Dawn.Engine.Basic.MathLib.CosShiftGenerator { StartNumber = YPosition += 100, EndNumber = YPosition += 50 };
			shift[2] = new Dawn.Engine.Basic.MathLib.SinShiftGenerator { StartNumber = YPosition += 100, EndNumber = YPosition += 50 };
			shift[3] = new Dawn.Engine.Basic.MathLib.CosShiftGenerator { StartNumber = YPosition += 100, EndNumber = YPosition += 50 };
			shiftRate[0] = 5;
			shiftRate[1] = 5;
			shiftRate[2] = 1;
			shiftRate[3] = 1;
			Percent = 0;

			font = DGE.Data.Cache.Fonts.GetHelper(DGE.Data.Cache.Font(new Dawn.Engine.Resource.Data.FontFamilyData(new System.Drawing.FontFamily("微软雅黑"), 24, System.Drawing.Color.White, false)));

			DGE.Graphics.UI.OnClick += UI_OnClick;
		}

		void UI_OnClick(object sender, Dawn.Engine.Manager.Processor.InputManager.MouseEventArgs e)
		{
			DGE.Scenes.Push<Scene_LyricShower>();
		}
		public override void End()
		{
			DGE.Graphics.UI.OnClick -= UI_OnClick;
			spr[0].Dispose();
			spr[1].Dispose();
			spr[2].Dispose();
			spr[3].Dispose();
			motion = null;
			base.End();
		}

		public override void Update()
		{
			base.Update();
			int k = 0;
			font.DrawString("FPS: " + DGE.Graphics.FPS.ToString(), 10, k);
			k += 50;
			for (int i = 0; i < 4; i++, k += 25)
			{
				font.DrawString("Sprite#" + i.ToString() + " X: " + motion[i].GetType().Name, 10, k);
			}
			k += 20;
			for (int i = 0; i < 4; i++, k += 25)
			{
				font.DrawString("Sprite#" + i.ToString() + " Y: " + shift[i].GetType().Name + " x Rate: " + shiftRate[i].ToString() + "Hz", 10, k);
			}

			spr[0].X = (float)motion[0].GetStepWithFix(ref Percent);
			spr[1].X = (float)motion[1].GetStep(Percent);
			spr[2].X = (float)motion[2].GetStep(Percent);
			spr[3].X = (float)motion[3].GetStep(Percent);
			spr[0].Y = (float)shift[0].GetStep(Percent * shiftRate[0]);
			spr[1].Y = (float)shift[1].GetStep(Percent * shiftRate[1]);
			spr[2].Y = (float)shift[2].GetStep(Percent * shiftRate[2]);
			spr[3].Y = (float)shift[3].GetStep(Percent * shiftRate[3]);
			Percent += 0.003;
		}
	}
}
