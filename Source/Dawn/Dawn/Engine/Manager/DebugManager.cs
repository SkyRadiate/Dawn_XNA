using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Manager
{
    public class DebugManager : EngineObject
    {
        public override string ObjectClassName() { return Define.EngineClassName.DebugManager(); }
        public DebugManager()
        {

        }

        public void Initialize()
        {

        }

        public void Error(EngineObject obj, string ErrorName, string ErrorDetail)
        {
            string err = obj.ObjectClassName() + Environment.NewLine + ErrorName + Environment.NewLine + ErrorDetail;
            throw (new SystemException(err));
        }

        public void Warning(EngineObject obj, string ErrorName, string ErrorDetail)
        {
            string err = obj.ObjectClassName() + Environment.NewLine + ErrorName + Environment.NewLine + ErrorDetail;
        }

		public void Debug(EngineObject obj, string DebugDetail)
		{
		}
    }
}
