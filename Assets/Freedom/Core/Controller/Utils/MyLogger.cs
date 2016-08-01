using System;

namespace Freedom.Core.Controller.Utils
{
    public class MyLogger
    {
        public static void Log(string s, params object[] values)
        {
            UnityEngine.Debug.Log (string.Format(s,values));
        }

        public static void Log(string s)
        {
            UnityEngine.Debug.Log (s);
        }
    }
}

