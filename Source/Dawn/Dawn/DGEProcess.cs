using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn
{
    static class DGEProcess
    {
        public static void Start()
        {
            Engine.Resource.AudioStream audios = new Engine.Resource.AudioStream(DGE.Data.Audio("tmp.mp3"));

            Engine.Resource.Audio audio = new Engine.Resource.Audio(DGE.Data.Audio("3711.mp3"));
            audio.Load();
            audios.Load();
            DGE.Audio.PlayBGS(audio);
            DGE.Audio.PlayBGM(audios);
        }

        public static void End()
        {

        }
    }
}
