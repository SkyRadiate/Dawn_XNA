using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
	public class ThreadManager : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.ThreadManager(); }
		System.Threading.Thread[] threads;
		Engine.Basic.ThreadProcessor.ThreadProcessor[] processors;
		Dictionary<Engine.Basic.ThreadProcessor.ThreadProcessor, int> dict;
		Random randomer;
		public ThreadManager()
		{
			threads=new System.Threading.Thread[Engine.Define.EngineConst.ThreadManager_MaxThreadNumber()];
			processors = new Basic.ThreadProcessor.ThreadProcessor[Engine.Define.EngineConst.ThreadManager_MaxThreadNumber()];
			dict = new Dictionary<Basic.ThreadProcessor.ThreadProcessor, int>(Engine.Define.EngineConst.ThreadManager_MaxThreadNumber());
			randomer = new Random();
		}

		public void Initialize()
		{
			
		}
		protected int FindFreeNum()
		{
			int id = randomer.Next(0, Engine.Define.EngineConst.ThreadManager_MaxThreadNumber() - 1);
			int j = id;
			do
			{
				if (processors[j] == null)
				{
					return j;
				}
				j++;
			} while (j != id);
			return -1;
		}
		public void NewThread(Engine.Basic.ThreadProcessor.ThreadProcessor processor)
		{
			int id = FindFreeNum();
			if (id == -1)
			{
				DGE.Debug.Error(this, Define.EngineErrorName.ThreadManager_TooManyThread(), Define.EngineErrorDetail.Empty());
			}

			processors[id] = processor;
			processors[id].OnEnd += new SimpleEventHandler(ThreadManager_OnEnd);
			dict.Add(processors[id], id);
			threads[id] = new System.Threading.Thread(new System.Threading.ThreadStart(processors[id].Process));

			threads[id].IsBackground = true;
			threads[id].Start();
		}

		public void ThreadManager_OnEnd(object Object, EventArgs e)
		{
			Engine.Basic.ThreadProcessor.ThreadProcessor processor = Object as Engine.Basic.ThreadProcessor.ThreadProcessor;
			int id;
			dict.TryGetValue(processor, out id);
			dict.Remove(processor);
			threads[id] = null;
			processors[id] = null;
		}

		public bool isEnd(Engine.Basic.ThreadProcessor.ThreadProcessor processor)
		{
			int id;
			dict.TryGetValue(processor, out id);
			if (id == -1 || processor == null || threads[id] == null)
			{
				return true;
			}
			return threads[id].ThreadState==System.Threading.ThreadState.Stopped;
		}
	}
}
