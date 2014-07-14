using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource
{
	public class LyricFile : Resource
	{
		public override string ObjectClassName() { return Define.EngineClassName.LyricFileResource(); }
		protected TimeSpan[] lyricTime;
		protected string[] lyricString;
		int nCount;

		public LyricFile()
			: base()
		{
		}

		public LyricFile(string filename)
			: base(filename)
		{

		}
		public override void Load()
		{
			base.Load();
			System.IO.FileStream loadStream = new System.IO.FileStream(filename, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read);
			System.IO.StreamReader reader = new System.IO.StreamReader(loadStream);

			nCount = 0;
			lyricTime = new TimeSpan[Engine.Define.EngineConst.LyricFileResource_MaxLine()];
			lyricString = new string[Engine.Define.EngineConst.LyricFileResource_MaxLine()];


			char[] map = new char[] { '[', ':', '.', ']' };
			while (!reader.EndOfStream)
			{
				string tmp = reader.ReadLine();
				string[] proc = tmp.Split(map);
				List<string>lst = new List<string>(proc);
				for (int i = 0; i < lst.Count; i++)
				{
					if(lst[i]=="")
					{
						lst.RemoveAt(i);
						i--;
					}
				}
				if (lst.Count == 4)
				{
					lyricTime[nCount] = new TimeSpan(0, 0, Convert.ToInt32(lst[0]), Convert.ToInt32(lst[1]), Convert.ToInt32(lst[2]) * 10);
					lyricString[nCount] = lst[3];
					nCount++;
				}
			}
		}

		public override void Unload()
		{
			lyricTime = null;
			lyricString = null;
			nCount = 0;
			base.Unload();
		}

		public string[] GetLyricString()
		{
			return lyricString;
		}

		public TimeSpan[] GetTimeSpan()
		{
			return lyricTime;
		}
		public int GetLyricCount()
		{
			return nCount;
		}
	}
}
