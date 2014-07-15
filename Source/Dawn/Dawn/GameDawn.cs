using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;



namespace Dawn
{
    public class GameDawn : Microsoft.Xna.Framework.Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public GameDawn()
        {
            graphics = new GraphicsDeviceManager(this);
			

            graphics.PreferredBackBufferHeight = Engine.Define.GameWindow.Height();
            graphics.PreferredBackBufferWidth = Engine.Define.GameWindow.Width();
			graphics.SynchronizeWithVerticalRetrace = Dawn.Engine.Define.GameConst.VSync();

			graphics.DeviceReset += new EventHandler<EventArgs>(graphics_DeviceReset);
			graphics.DeviceResetting += new EventHandler<EventArgs>(graphics_DeviceResetting);

			Content.RootDirectory = Dawn.Engine.Manager.DataManager.ContentPath();

			this.IsMouseVisible = Engine.Define.GameConst.ShowCursor();
			this.IsFixedTimeStep = Dawn.Engine.Define.GameConst.LimitFPS();
			if(Dawn.Engine.Define.GameConst.LimitFPS())this.TargetElapsedTime = new TimeSpan(10000000 / Dawn.Engine.Define.GameConst.FramePerSecond());
        }

		void graphics_DeviceResetting(object sender, EventArgs e)
		{
			DGE.Graphics.graphics_DeviceResetting(sender, e);
		}

		void graphics_DeviceReset(object sender, EventArgs e)
		{
			DGE.Graphics.graphics_DeviceReset(sender, e);
		}

		~GameDawn()
		{
			DGEProcess.End();
		}
        protected override void Initialize()
        {
			Window.Title = Engine.Define.GameConst.GameTitleName();
			
            Engine.Others.GameTool.SetWindowPosition(Window, Engine.Define.GameWindow.StartPositionX(Window), Engine.Define.GameWindow.StartPositionY(Window));

            base.Initialize();

			DGE.Initialize();
			DGEProcess.Start();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void UnloadContent()
        {
			
        }
        protected override void Update(GameTime gameTime)
        {
			base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {

            DGE.Update();
            base.Draw(gameTime);
        }
		public ContentManager _ContentManager { get { return Content; } }
		public SpriteBatch _SpriteBatch { get { return spriteBatch; } }
		public MouseState _MouseInputState { get { return Mouse.GetState(); } }

		public void _SetMousePosition(int x,int y)
		{
			Mouse.SetPosition(x, y);
		}

		public GraphicsDevice _GraphicsDevice { get { return GraphicsDevice; } }
    }
}
