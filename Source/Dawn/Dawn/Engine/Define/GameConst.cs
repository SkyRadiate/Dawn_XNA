using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Define
{

	static public class GameConst
	{
		public static string GameName() { return "TRUE"; }

		public static string GameTitleName()
		{
			string Temp = "";
#if DEBUG
   Temp += " [";

   Temp += "Debug";

#if WIN64
   Temp += " x64";
#else
   Temp += " x86";
#endif
   Temp += "]";
#endif

			return GameName() + Temp;
		}

		public static bool ShowCursor() { return false; }

		public static int FramePerSecond() { return 60; }
		public static bool LimitFPS() { return true; }
		public static bool VSync() { return true; }
	}
}
