using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dawn.Engine.Resource;
using Microsoft.Xna.Framework;

namespace Dawn.Engine.Manager
{
	public class InputManager : EngineObject
    {
		private Texture mouseTexture;
		private Texture mouseBusyTexture;
		private bool _Busy;
		
        public override string ObjectClassName() { return Define.EngineClassName.InputManager(); }

		public void SetBusy(bool busy)
		{
			_Busy = busy;
		}
        public InputManager()
        {
			
        }
        public void Initialize()
        {
			DGE.Engine.Start += new SimpleEventHandler(OnInit);
        }

		private void OnInit(object s, EventArgs e)
		{
			mouseTexture = DGE.Cache.Graphics(@"Texture\System\Cursor");
			mouseBusyTexture = DGE.Cache.Graphics(@"Texture\System\Cursor_Busy");
			DGE.Graphics.PostRender += new SimpleEventHandler(RenderMouse);
		}

		private void RenderMouse(object sender,EventArgs e)
		{
			int mouseX, mouseY;
			mouseX = MouseState.X;
			mouseY = MouseState.Y;
			if (_Busy)
			{
				DGE.Graphics.Draw(mouseBusyTexture, new Vector2(mouseX, mouseY), Color.White);
			}
			else
			{
				DGE.Graphics.Draw(mouseTexture, new Vector2(mouseX, mouseY), Color.White);
			}
		}

		public Microsoft.Xna.Framework.Input.GamePadState State
		{
			get { return Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One); }
		}
		public Microsoft.Xna.Framework.Input.KeyboardState KeyBoardState
		{
			get { return Microsoft.Xna.Framework.Input.Keyboard.GetState(PlayerIndex.One); }
		}
		public Microsoft.Xna.Framework.Input.MouseState MouseState
		{
			get { return Microsoft.Xna.Framework.Input.Mouse.GetState(); }
		}
    }
}
