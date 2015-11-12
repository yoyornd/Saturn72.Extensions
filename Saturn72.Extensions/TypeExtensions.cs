using System;

namespace Saturn72.Extensions
{
    public static class TypeExtensions
    {
        public static object GetDefault(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
