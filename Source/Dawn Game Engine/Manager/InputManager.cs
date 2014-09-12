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

		public Processor.InputManager.MouseEventArgs MouseArgs { get; private set; }

        public override string ObjectClassName() { return Define.EngineClassName.InputManager(); }

		public void SetBusy(bool busy)
		{
			_Busy = busy;
		}
        public InputManager()
        {
			MouseArgs = new Processor.InputManager.MouseEventArgs
			{
				ButtonLeft = Processor.InputManager.MouseButtonStatus.Released,
				ButtonRight = Processor.InputManager.MouseButtonStatus.Released,
				ButtonMiddle = Processor.InputManager.MouseButtonStatus.Released,
				X = 0,
				Y = 0
			};
        }
        public void Initialize()
        {
			DGE.Engine.Start += new SimpleEventHandler(OnInit);
        }
		public void Update()
		{
			MouseArgs = new Processor.InputManager.MouseEventArgs
			{
				ButtonLeft = this.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed ? Processor.InputManager.MouseButtonStatus.Pressed: Processor.InputManager.MouseButtonStatus.Released,
				ButtonRight = this.MouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed ? Processor.InputManager.MouseButtonStatus.Pressed : Processor.InputManager.MouseButtonStatus.Released,
				ButtonMiddle = this.MouseState.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed ? Processor.InputManager.MouseButtonStatus.Pressed : Processor.InputManager.MouseButtonStatus.Released,
				X = this.MouseState.X,
				Y = this.MouseState.Y
			};
		}
		private void OnInit(object s, EventArgs e)
		{
			mouseTexture = DGE.Cache.Graphics(DGE.Data.SystemTexture("Cursor"));
			mouseBusyTexture = DGE.Cache.Graphics(DGE.Data.SystemTexture("Cursor_Busy"));
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
