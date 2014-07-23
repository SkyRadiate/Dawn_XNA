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
		protected string GetTypeInformation(EngineObject obj)
		{
			if (obj == null) return "Null";
			return obj.GetType().FullName;
		}
		public void Error(EngineObject obj, string ErrorName, string ErrorDetail)
		{
			string err = GetTypeInformation(obj) + "[" + obj.ObjectClassName() + "]" + "\n" + ErrorName + "\n" + ErrorDetail;
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
