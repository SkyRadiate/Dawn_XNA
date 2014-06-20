using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FMOD;

namespace Dawn.Engine.Manager
{
    class AudioManager : EngineObject
    {
        private FMOD.System system = null;
        private FMOD.Sound sound = null;
        private FMOD.Channel channel = null;
        public AudioManager()
        {
        }

        public void Initialize()
        {
            uint            version = 0;

            /*
                Global Settings
            */
            FMODRun(FMOD.Factory.System_Create(ref system));

            FMODRun(system.getVersion(ref version));
            if (version < FMOD.VERSION.number)
            {
                //MessageBox.Show("Error!  You are using an old version of FMOD " + version.ToString("X") + ".  This program requires " + FMOD.VERSION.number.ToString("X") + ".");
            }

            FMODRun(system.init(1, FMOD.INITFLAGS.NORMAL, (IntPtr)null));

            FMODRun(system.createStream("../../Data/Audio/tmp.mp3", FMOD.MODE._2D, ref sound));
            FMODRun(system.playSound(FMOD.CHANNELINDEX.FREE, sound, false, ref channel));
        }

        public void FMODRun(FMOD.RESULT result)
        {
            if (result != FMOD.RESULT.OK)
            {
                throw(new SystemException("FMOD error! " + result + " - " + FMOD.Error.String(result)));
            }
        }
    }
}
