using System.Collections.Generic;
using System.Linq;

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
    }
}