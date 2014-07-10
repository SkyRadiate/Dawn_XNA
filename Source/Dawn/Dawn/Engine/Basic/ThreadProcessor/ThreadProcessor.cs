using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dawn.Engine.Manager;

namespace Dawn.Engine.Basic.ThreadProcessor
{
	public class ThreadProcessor : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.ThreadProcessor(); }

		public event SimpleEventHandler OnEnd;
		protected bool _isEnd;

		public ThreadProcessor()
		{
			_isEnd = false;
		}

		public void Process()
		{
			if(!_isEnd)
			{
				Update();
			}
		}

		public virtual void Update()
		{
			EndUpdate();
		}

		protected void EndUpdate()
		{
			_isEnd = true;
			EventArgs e = new EventArgs();
			if (OnEnd != null)
			{
				OnEnd(this, e);
			}
		}

		public bool isEnd { get { return _isEnd; } }

	}
}
