using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Resource
{
    class Resource : EngineObject
    {
        private bool _isLoad;
        public Resource()
        {
            _isLoad = false;
        }

        public void Dispose()
        {
            if(isLoad())
            {
                Unload();
            }
        }

        ~Resource()
        {
            Dispose();
        }

        public void Load()
        {
            _isLoad = true;
        }

        public void Unload()
        {
            _isLoad = false;
        }

        public bool isLoad()
        {
            return _isLoad;
        }

        public bool GetResource()
        {
            if (isLoad())
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}
