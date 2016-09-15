using System;
using System.Diagnostics;
using System.Reflection;

namespace Saturn72.Extensions
{
    public class RuntimeUtil
    {
        public static MethodBase GetCallerMethodInfo()
        {
            return GetStackTraceFrame(2).GetMethod();
        }

        public static StackFrame GetStackTraceFrame(int depth)
        {
            var stackTrace = new StackTrace();
            if (depth >= stackTrace.FrameCount)
                throw new ArgumentException(string.Format("Request dept is too deep {0}. Maximum depth is {1}", depth,
                    stackTrace.FrameCount));

            return stackTrace.GetFrame(depth);
        }
    }
}