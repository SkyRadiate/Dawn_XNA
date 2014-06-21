using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawn.Engine.Define
{
    static class EngineErrorDetail
    {
        public static string Empty() { return ""; }

        public static string FMODError() { return "FMODError"; }

        public static string Filename() { return "Filename:"; }

        public static string Separator() { return ": "; }
        public static string NewlineSeparator() { return Environment.NewLine; }
    }
}
