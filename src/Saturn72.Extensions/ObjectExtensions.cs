using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Saturn72.Extensions
{
    public static class ObjectExtensions
    {
        public static bool NotNull<T>(this T obj) where T : class
        {
            return !IsNull(obj);
        }

        public static bool IsNull<T>(this T obj) where T : class
        {
            return obj == null;
        }

        public static bool IsDefault<T>(this T obj)
        {
            return obj == null || obj.Equals(default(T));
        }

        public static IDictionary<string, object> ToPropertyDictionary(this object obj)
        {
            var result = new Dictionary<string, object>();

            var pInfos = obj.GetType().GetProperties()
                .Where(propertyInfo => propertyInfo.CanRead && propertyInfo.GetIndexParameters().Length == 0);

            foreach (var pi in pInfos)
                result[pi.Name] = pi.GetValue(obj, null);

            return result;
        }

        public static MethodBase GetCallerMethodInfo(this object obj)
        {
            return GetStackTraceFrame(obj,1).GetMethod() as MethodBase;
        }

        public static StackFrame GetStackTraceFrame(this object o,int depth)
        {
            var stackTrace = new StackTrace();
            if(depth>= stackTrace.FrameCount)
                throw new ArgumentException(string.Format("Request dept is too deep {0}. Maximum depth is {1}", depth, stackTrace.FrameCount));

            return stackTrace.GetFrame(depth);
        }
    }
}