using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource
{
    public class AudioStream : Audio
    {
        public override string ObjectClassName() { return Define.EngineClassName.AudioStreamResource(); }

        public AudioStream()
            : base()
        {
            Stream = true;
        }

        public AudioStream(string filename)
            : base(filename)
        {
            Stream = true;
        }

        ~AudioStream()
        {
            Dispose();
        }
    }
}
