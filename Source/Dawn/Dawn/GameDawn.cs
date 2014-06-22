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


			Content.RootDirectory = Dawn.Engine.Manager.DataManager.ContentPath();

			this.IsMouseVisible = Engine.Define.GameConst.ShowCursor();
			this.IsFixedTimeStep = true;
			this.TargetElapsedTime = new TimeSpan(10000000 / Dawn.Engine.Define.GameConst.FramePerSecond());
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
            GraphicsDevice.Clear(Engine.Define.GameWindow.BackgroundColor());
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
    }
}
