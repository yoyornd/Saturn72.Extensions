using System.Collections.Generic;
using System.Linq;

namespace Saturn72.Extensions
{
    public static class DynamicHelper
    {
        public static IDictionary<string, object> ToPropertyDictionary(dynamic dObj)
        {

            var result = new Dictionary<string, object>();

            var pInfos = (dObj as object).GetType().GetProperties()
                .Where(propertyInfo => propertyInfo.CanRead && propertyInfo.GetIndexParameters().Length == 0);

            foreach (var pi in pInfos)
                result[pi.Name] = pi.GetValue(dObj, null);

            return result;
        }

    }
}
