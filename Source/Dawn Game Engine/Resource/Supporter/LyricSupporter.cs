using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource.Supporter
{
	public class LyricSupporter : EngineObject
	{
		public override string ObjectClassName() { return Define.EngineClassName.LyricSupporter(); }

		string[] lyricString;
		double[] lyricTime;
		int n;
		int _offset;
		public LyricSupporter(Dawn.Engine.Resource.LyricFile lrcFile, int offset = 0)
		{
			Initialize(lrcFile, offset);
		}

		public void Initialize(Dawn.Engine.Resource.LyricFile lrcFile, int offset = 0)
		{
			n = lrcFile.GetLyricCount();
			lyricString = lrcFile.GetLyricString();
			lyricTime = new double[n];
			for (int i = 0; i < n; i++)
			{
				lyricTime[i] = lrcFile.GetTimeSpan()[i].TotalMilliseconds;
			}
			_offset = offset;
		}

		public string GetLyric(double milliseconds)
		{
			for (int i = 0; i < n; i++)
			{
				if (lyricTime[i] >= milliseconds - _offset)
				{
					if (i - 1 < 0) return "";
					return lyricString[i - 1];
				}
			}
			return "";
		}
	}
}
