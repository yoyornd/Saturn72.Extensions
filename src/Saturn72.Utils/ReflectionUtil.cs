#region

using System.Reflection;

#endregion

namespace Saturn72.Utils
{
    public static class ReflectionUtil
    {
        public static void SetPropertyValueUsingReflection(object obj, string name, object value)
        {
            var pInfo = obj.GetType()
                .GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            var finalValue = pInfo.PropertyType.IsInstanceOfType(value)
                ? value
                : CommonHelper.GetCustomTypeConverter(pInfo.PropertyType).ConvertFrom(value);

            pInfo.SetValue(obj, finalValue, null);
        }
    }
}