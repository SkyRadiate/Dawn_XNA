using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Define
{
    static class GameConst
    {
        public static string GameName() { return "TRUE"; }

        public static string GameTitleName()
        {
            string Temp = "";
#if DEBUG
            Temp += "[";

            Temp += "Debug";

#if WIN64
            Temp += " x64";
#else
            Temp += " x86";
#endif
            Temp += "]";
#endif

            return GameName() + " " + Temp;
        }
    }
}
