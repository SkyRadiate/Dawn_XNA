using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource
{
    public class Audio : Resource
    {
        public override string ObjectClassName() { return Define.EngineClassName.AudioResource(); }

        protected bool Stream;
        protected FMOD.Sound resource_sound;

        public Audio()
            : base()
        {
            Stream = false;
        }

        public Audio(string filename)
            : base(filename)
        {
            Stream = false;
        }

        ~Audio()
        {
            Dispose();
        }

        public override void Load()
        {
            base.Load();
            if (Stream)
            {
                FMODRun(DGE.Audio.AudioSystem.createStream(_filename, FMOD.MODE.DEFAULT | FMOD.MODE.LOOP_NORMAL, ref resource_sound));
            }
            else
            {
                FMODRun(DGE.Audio.AudioSystem.createSound(_filename, FMOD.MODE.DEFAULT | FMOD.MODE.LOOP_OFF, ref resource_sound));
            }
        }
        public override void Unload()
        {
            FMODRun(resource_sound.release());
            base.Unload();
        }
        public void FMODRun(FMOD.RESULT result)
        {
            if (result != FMOD.RESULT.OK)
            {
                DGE.Debug.Error(this, Define.EngineErrorName.AudioManager_FMODError(), Define.EngineErrorDetail.FMODError() + Define.EngineErrorDetail.Separator() + FMOD.Error.String(result) + Define.EngineErrorDetail.NewlineSeparator() + GetErrorDetail());
            }
        }

        public FMOD.Sound GetSound()
        {
            if (isLoad())
            {
                return resource_sound;
            }
            else
            {
                DGE.Debug.Error(this, Define.EngineErrorName.Resource_CannotGetResource(), GetErrorDetail());
                return null;
            }
        }
    }
}
