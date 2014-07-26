using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
	public class UIManager : EngineObject
	{

		Processor.InputManager.MouseEventArgs lstArgs;
		Processor.InputManager.MouseEventArgs Args;

		public event MouseEventHandler OnMouseDown;
		public event MouseEventHandler OnMouseUp;
		public event MouseEventHandler OnClick;
		public event MouseEventHandler OnMouseMove;

		private List<Dawn.Engine.Basic.Sprite> SpriteSet;

		public void Register(Dawn.Engine.Basic.UIObject spr)
		{
			int index = SpriteSet.IndexOf(spr);
			if (index == -1)
			{
				SpriteSet.Add(spr);
				SpriteSet.Sort(new Processor.SpriteManager.SpriteZComparer());
			}
		}
		public void UnRegister(Dawn.Engine.Basic.Sprite spr)
		{
			SpriteSet.Remove(spr);
		}
		public void ReRegister(Dawn.Engine.Basic.Sprite spr)
		{
			UnRegister(spr);
			Register(spr);
		}
		public UIManager()
		{
			lstArgs = Args = null;
			SpriteSet = new List<Basic.Sprite>();
		}

		public void Initialize()
		{
			lstArgs = DGE.Input.MouseArgs;
		}

		public void Update()
		{
			Args = DGE.Input.MouseArgs;

			if (Args.X != lstArgs.X || Args.Y!=lstArgs.Y)
			{
				if (OnMouseMove != null)
				{
					OnMouseMove(this, Args);
				}
			}

			bool needDown, needUp;
			needDown = needUp = false;
			if (Args.ButtonLeft != lstArgs.ButtonLeft)
			{
				if (Args.ButtonLeft == Processor.InputManager.MouseButtonStatus.Pressed)
				{
					needDown = true;
				}
				else
				{
					needUp = true;
				}
			}
			if (Args.ButtonRight != lstArgs.ButtonRight)
			{
				if (Args.ButtonRight == Processor.InputManager.MouseButtonStatus.Pressed)
				{
					needDown = true;
				}
				else
				{
					needUp = true;
				}
			}
			if (Args.ButtonMiddle != lstArgs.ButtonMiddle)
			{
				if (Args.ButtonMiddle == Processor.InputManager.MouseButtonStatus.Pressed)
				{
					needDown = true;
				}
				else
				{
					needUp = true;
				}
			}
			if(needDown)
			{
				if (OnMouseDown != null)
				{
					OnMouseDown(this, Args);
				}
			}
			if(needUp)
			{
				if (OnMouseUp != null)
				{
					OnMouseUp(this, Args);
				}
				if (OnClick != null)
				{
					OnClick(this, Args);
				}
			}
			lstArgs = (Processor.InputManager.MouseEventArgs)Args.Clone();

			for (int i = SpriteSet.Count - 1; i >= 0; i--)
			{
				SpriteSet[i].Update();
			}
		}
	}
}
