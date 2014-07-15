using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Define
{
	static public class EngineConst
    {
        public static int Version() { return 0;}
        public static int AudioManager_MaxChannels() { return 20; }

        public enum AudioManager_ChannelType
        {
            UnknownMusic = 0,
            UnknownEffect,
            Empty,
            BGM, BGS, BGE,
            ME, SE
        };

		public static int FontHelper_TextureWidth() { return 50; }
		public static int FontHelper_TextureHeight() { return 50; }
		public static int FontHelper_TextureNum() { return 1; }
		public static int ThreadManager_MaxThreadNumber() { return 100; }
		public static int LyricFileResource_MaxLine() { return 100; }
    }
}
