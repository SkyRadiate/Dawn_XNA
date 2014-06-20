using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FMOD;
using Dawn.Engine;

namespace Dawn.Engine.Manager
{
    class AudioManager : EngineObject
    {
        public override string ObjectClassName() { return Define.EngineClassName.AudioManager(); }

        private FMOD.System system = null;
        private FMOD.Sound sound = null;
        private FMOD.Channel []channel = null;
        private Define.EngineConst.AudioManager_ChannelType []channelType;
        public AudioManager()
        {
            channelType = new Define.EngineConst.AudioManager_ChannelType[Define.EngineConst.AudioManager_MaxChannels()];
            for (int i = 0; i < Define.EngineConst.AudioManager_MaxChannels(); i++)
            {
                channelType[i] = Define.EngineConst.AudioManager_ChannelType.Empty;
            }
        }

        ~AudioManager()
        {
            if (system != null)
            {
                FMODRun(system.close());
                FMODRun(system.release());
            }
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

            FMODRun(system.setSoftwareChannels(100));
            FMODRun(system.setHardwareChannels(64));
            FMODRun(system.init(200, FMOD.INITFLAGS.NORMAL, (IntPtr)0));
            /*
            FMODRun(system.createStream("../../Data/Audio/tmp.mp3", FMOD.MODE._2D, ref sound));
            FMODRun(system.playSound(FMOD.CHANNELINDEX.FREE, sound, false, ref channel));\
             */
        }

        public void FMODRun(FMOD.RESULT result)
        {
            if (result != FMOD.RESULT.OK)
            {
                DGE.Debug.Error(this, Define.EngineErrorName.AudioManager_FMODError(), FMOD.Error.String(result));
            }
        }
    }
}
