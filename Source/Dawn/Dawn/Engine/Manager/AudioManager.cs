using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FMOD;
using Dawn.Engine;

namespace Dawn.Engine.Manager
{
    public class AudioManager : EngineObject
    {
        public override string ObjectClassName() { return Define.EngineClassName.AudioManager(); }

        private FMOD.System system ;
        private FMOD.Channel[] channel;
        private Define.EngineConst.AudioManager_ChannelType []channelType;
		private Processor.AudioManager.AudioProcessor[] Processor;
        public AudioManager()
        {
            channel = new FMOD.Channel[Define.EngineConst.AudioManager_MaxChannels()];
            channelType = new Define.EngineConst.AudioManager_ChannelType[Define.EngineConst.AudioManager_MaxChannels()];
			Processor = new Processor.AudioManager.AudioProcessor[Define.EngineConst.AudioManager_MaxChannels()];			
			for (int i = 0; i < Define.EngineConst.AudioManager_MaxChannels(); i++)
            {
                channelType[i] = Define.EngineConst.AudioManager_ChannelType.Empty;
				channel[i] = new FMOD.Channel();
				Processor[i] = null;
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

        public FMOD.System AudioSystem { get { return system; } }

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
                DGE.Debug.Error(this, Define.EngineErrorName.AudioManager_FMODVersionError(), Define.EngineErrorDetail.Empty());
            }

            FMODRun(system.setSoftwareChannels(100));
            FMODRun(system.setHardwareChannels(64));
            FMODRun(system.init(200, FMOD.INITFLAGS.NORMAL, (IntPtr)0));
        }

        public void FMODRun(FMOD.RESULT result)
        {
            if (result != FMOD.RESULT.OK)
            {
                DGE.Debug.Error(this, Define.EngineErrorName.AudioManager_FMODError(), Define.EngineErrorDetail.FMODError() + Define.EngineErrorDetail.Separator() + FMOD.Error.String(result));
            }
        }

        protected void __Play(int channelID, Resource.Audio audio)
        {
            //FMODRun(system.playSound(CHANNELINDEX.REUSE, audio.GetSound(), false, ref ChannelID()[channelID]));
			FMOD.Channel tmpChannel = _ChannelID(channelID);
			FMODRun(system.playSound(CHANNELINDEX.FREE, audio.GetSound(), false, ref tmpChannel));
        }

        protected int FindChannel(Define.EngineConst.AudioManager_ChannelType type)
        {
            for (int i = 0; i < Define.EngineConst.AudioManager_MaxChannels(); i++)
            {
                if(channelType[i] == type)
                {
                    return i;
                }
            }
            return -1;
        }

        protected int FindFreeChannel()
        {
            return FindChannel(Define.EngineConst.AudioManager_ChannelType.Empty);
        }

        /*
        protected void ChannelID(int id, out FMOD.Channel Channel)
        {
            Channel = channel[id];
        }
         * */
		protected FMOD.Channel _ChannelID(int id)
		{
			return channel[id];
		}
		/*
        protected FMOD.Channel[] ChannelID()
        {
            return channel;
        }*/

        protected void _Play(Engine.Define.EngineConst.AudioManager_ChannelType type, Resource.Audio audio)
        {
            int channelID = FindChannel(type);
            if (channelID == -1) channelID = FindFreeChannel();
            if (channelID == -1)
            {
                DGE.Debug.Error(this, Define.EngineErrorName.AudioManager_ChannelNotEnough(), Define.EngineErrorDetail.Empty());
            }
            channelType[channelID] = type;
            __Play(channelID, audio);
        }

        protected void __Stop(Engine.Define.EngineConst.AudioManager_ChannelType type)
        {
            int channelID = FindChannel(type);
            while (channelID != -1)
            {
				FMOD.Channel tmpChannel = _ChannelID(channelID);
				tmpChannel.stop();
                channelType[channelID] = Define.EngineConst.AudioManager_ChannelType.Empty;
                channelID = FindChannel(type);
            }
        }
        public void Play(Engine.Define.EngineConst.AudioManager_ChannelType type,Resource.Audio audio)
        {
            _Play(type, audio);
        }

        public void Stop(Engine.Define.EngineConst.AudioManager_ChannelType type)
        {
            __Stop(type);
        }
        public void PlayUnknownMusic(Resource.Audio audio)
        {
            Play(Define.EngineConst.AudioManager_ChannelType.UnknownMusic, audio);
        }
        public void PlayUnknownEffect(Resource.Audio audio)
        {
            Play(Define.EngineConst.AudioManager_ChannelType.UnknownEffect, audio);
        }

        public void PlayBGM(Resource.Audio audio)
        {
            Play(Define.EngineConst.AudioManager_ChannelType.BGM, audio);
        }
        public void PlayBGS(Resource.Audio audio)
        {
            Play(Define.EngineConst.AudioManager_ChannelType.BGS, audio);
        }
        public void PlayBGE(Resource.Audio audio)
        {
            Play(Define.EngineConst.AudioManager_ChannelType.BGE, audio);
        }
        public void PlaySE(Resource.Audio audio)
        {
            Play(Define.EngineConst.AudioManager_ChannelType.SE, audio);
        }
        public void PlayME(Resource.Audio audio)
        {
            Play(Define.EngineConst.AudioManager_ChannelType.ME, audio);
        }
        public void StopUnknownMusic()
        {
            Stop(Define.EngineConst.AudioManager_ChannelType.UnknownMusic);
        }
        public void StopUnknownEffect()
        {
            Stop(Define.EngineConst.AudioManager_ChannelType.UnknownEffect);
        }
        public void StopBGM()
        {
            Stop(Define.EngineConst.AudioManager_ChannelType.BGM);
        }
        public void StopBGS()
        {
            Stop(Define.EngineConst.AudioManager_ChannelType.BGS);
        }
        public void StopBGE()
        {
            Stop(Define.EngineConst.AudioManager_ChannelType.BGE);
        }
        public void StopME()
        {
            Stop(Define.EngineConst.AudioManager_ChannelType.ME);
        }
        public void StopSE()
        {
            Stop(Define.EngineConst.AudioManager_ChannelType.SE);
        }
        public void Update()
        {
            system.update();

            for (int i = 0; i < Define.EngineConst.AudioManager_MaxChannels(); i++)
            {
                if (channelType[i] != Define.EngineConst.AudioManager_ChannelType.Empty)
                {
                    bool tmp = false;
                    
                    channel[i].isPlaying(ref tmp);
                    if (tmp == false || channel[i]==null)
                    {
                        channelType[i] = Define.EngineConst.AudioManager_ChannelType.Empty;
                    }
                }
            }

			UpdateProcessor();
        }

		private void UpdateProcessor()
		{

			for (int i = 0; i < Define.EngineConst.AudioManager_MaxChannels(); i++)
			{
				if (channelType[i] != Define.EngineConst.AudioManager_ChannelType.Empty)
				{
					if (Processor[i] != null)
					{
						if(Processor[i].isEnd())
						{
							Processor[i] = null;
						}
						else
						{
							Processor[i].Update();
						}
					}
				}
				else
				{
					if(Processor[i]!=null)
					{
						Processor[i] = null;
					}
				}
			}
		}

		public void FadeInPlay(Engine.Define.EngineConst.AudioManager_ChannelType type, Resource.Audio audio)
		{
			_FadeInPlay(type, audio);
		}

		protected void _FadeInPlay(Engine.Define.EngineConst.AudioManager_ChannelType type, Resource.Audio audio)
		{
			int channelID = FindChannel(type);
			if (channelID == -1) channelID = FindFreeChannel();
			if (channelID == -1)
			{
				DGE.Debug.Error(this, Define.EngineErrorName.AudioManager_ChannelNotEnough(), Define.EngineErrorDetail.Empty());
			}

			channelType[channelID] = type;
			__Play(channelID, audio);

			FMOD.Channel tmpChannel= _ChannelID(channelID);
			FMODRun(tmpChannel.setVolume(0.0f));
			Processor[channelID] = new Manager.Processor.AudioManager.AudioProcessor_FadeIn(ref tmpChannel);
		}

		public void FadeOutStop(Engine.Define.EngineConst.AudioManager_ChannelType type)
		{
			int channelID = FindChannel(type);
			//while (channelID != -1)
			{
				FMOD.Channel tmpChannel = _ChannelID(channelID);

				Processor[channelID] = new Manager.Processor.AudioManager.AudioProcessor_FadeOut(ref tmpChannel);
				//channelID = FindChannel(type);
			}
		}
    }
}
