using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
    class GraphicsManager : EngineObject
    {
        public override string ObjectClassName() { return Define.EngineClassName.GraphicsManager(); }

        private SceneManager _Scenes;


		public event SimpleEventHandler StartUpdate;
		public event SimpleEventHandler PreRender;
		public event SimpleEventHandler PostRender;
		public event SimpleEventHandler EndUpdate;

		public Microsoft.Xna.Framework.Graphics.SpriteBatch Canvas { get { return DGE.Game._SpriteBatch; } }
        public SceneManager Scenes { get { return _Scenes; } }
        public GraphicsManager()
        {
            _Scenes=new SceneManager();
        }

        public void Initialize()
        {
            _Scenes.Initialize();
        }

		protected void OnStartUpdate()
		{
			if (StartUpdate != null)
			{
				EventArgs e = new EventArgs();
				StartUpdate(this, e);
			}
		}
		protected void OnPreRender()
		{
			if (PreRender != null)
			{
				EventArgs e = new EventArgs();
				PreRender(this, e);
			}
		}
		protected void OnPostRender()
		{
			if (PostRender != null)
			{
				EventArgs e = new EventArgs();
				PostRender(this, e);
			}
		}
		protected void OnEndUpdate()
		{
			if (EndUpdate != null)
			{
				EventArgs e = new EventArgs();
				EndUpdate(this, e);
			}
		}
		public void Update()
		{
			OnStartUpdate();

			Canvas.Begin();
			OnPreRender();

			OnPostRender();
			Canvas.End();
			OnEndUpdate();
		}
    }
}
