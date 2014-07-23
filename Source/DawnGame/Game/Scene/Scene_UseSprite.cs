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
		double Percent;

		Dawn.Engine.Manager.Processor.FontManager.FontHelper font;
		public Scene_UseSprite()
		{

		}
		public override void Start()
		{
			base.Start();
			spr = new Dawn.Engine.Basic.ExtendSprite[4];
			spr[0] = new Dawn.Engine.Basic.ExtendSprite(DGE.Data.Cache.Graphics(DGE.Data.SystemTexture("Dawn Logo Blue")), y: 0f, z: 4);
			spr[1] = new Dawn.Engine.Basic.ExtendSprite(DGE.Data.Cache.Graphics(DGE.Data.SystemTexture("Dawn Logo Blue")), y: 100f, z: 3);
			spr[2] = new Dawn.Engine.Basic.ExtendSprite(DGE.Data.Cache.Graphics(DGE.Data.SystemTexture("Dawn Logo Blue")), y: 200f, z: 2);
			spr[3] = new Dawn.Engine.Basic.ExtendSprite(DGE.Data.Cache.Graphics(DGE.Data.SystemTexture("Dawn Logo Blue")).Clone()as Dawn.Engine.Resource.Texture , y: 300f, z: 1);
			motion = new Dawn.Engine.Basic.MathLib.MotionGenerator[4];
			motion[0] = new Dawn.Engine.Basic.MathLib.SinEaseMotionGenerator { StartNumber = 0, EndNumber = DGE.Graphics.Width() - spr[0].tex.Width() };
			motion[1] = new Dawn.Engine.Basic.MathLib.SinEaseinMotionGenerator { StartNumber = 0, EndNumber = DGE.Graphics.Width() - spr[0].tex.Width() };
			motion[2] = new Dawn.Engine.Basic.MathLib.SinEaseoutMotionGenerator { StartNumber = 0, EndNumber = DGE.Graphics.Width() - spr[0].tex.Width() };
			motion[3] = new Dawn.Engine.Basic.MathLib.MotionGenerator { StartNumber = 0, EndNumber = DGE.Graphics.Width() - spr[0].tex.Width() };
			Percent = 0;

			font = DGE.Data.Cache.Fonts.GetHelper(DGE.Data.Cache.Font(new Dawn.Engine.Resource.Data.FontFamilyData(new System.Drawing.FontFamily("微软雅黑"), 28, System.Drawing.Color.White, false, false, false)));
		}
		public override void End()
		{
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
			for (int i = 0,k=0; i < 4;i++ ,k+=30)
			{
				font.DrawString("Row"+i.ToString()+ ": "+motion[i].GetType().Name, 10, k + 10);
			}
				spr[0].X = (float)motion[0].GetStepWithFix(ref Percent);
			spr[1].X = (float)motion[1].GetStepWithFix(ref Percent);
			spr[2].X = (float)motion[2].GetStepWithFix(ref Percent);
			spr[3].X = (float)motion[3].GetStepWithFix(ref Percent);
			Percent += 0.003;
		}
	}
}
