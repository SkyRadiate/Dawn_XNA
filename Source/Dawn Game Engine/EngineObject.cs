using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Dawn.Engine
{
    public class EngineObject : Object
    {
        public virtual string ObjectClassName() { return Define.EngineClassName.Empty(); }
    }
}
