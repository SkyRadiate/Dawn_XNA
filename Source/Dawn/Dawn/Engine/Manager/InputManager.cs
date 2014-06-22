using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dawn.Engine.Manager
{
    class InputManager : EngineObject
    {
		private Texture2D mouseTexture;
        public override string ObjectClassName() { return Define.EngineClassName.InputManager(); }
        public InputManager()
        {
			
        }
        public void Initialize()
        {
			DGE.Engine.Start += new SimpleEventHandler(OnInit);
        }

		private void OnInit(object s, EventArgs e)//声明一个符合事件委托签名的处理方法
		{
			mouseTexture = DGE.Cache.Graphics(@"Texture\System\Cursor");
			DGE.Graphics.PostRender += new SimpleEventHandler(RenderMouse);
		}

		private void RenderMouse(object sender,EventArgs e)
		{
			DGE.Graphics.Canvas.Draw(mouseTexture, new Vector2(DGE.Game._MouseInputState.X, DGE.Game._MouseInputState.Y), Color.White);
		}
    }
}
