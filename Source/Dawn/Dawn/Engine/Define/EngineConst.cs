using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Define
{
    static class EngineConst
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
    }
}
