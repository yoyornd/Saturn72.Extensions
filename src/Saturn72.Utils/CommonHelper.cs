#region

using System;
using System.Collections.Generic;
using System.ComponentModel;

#endregion

namespace Saturn72.Extensions
{
    public static class CommonHelper
    {
        private static readonly IDictionary<Type, Func<TypeConverter>> CustomTypeConverters = new Dictionary
            <Type, Func<TypeConverter>>
        {
            {typeof(List<int>), () => new GenericListTypeConverter<int>()},
            {typeof(List<decimal>), () => new GenericListTypeConverter<string>()},
            {typeof(List<string>), () => new GenericListTypeConverter<string>()},
            {typeof(decimal), () => new DecimalConverter()}
        };

        public static TypeConverter GetCustomTypeConverter(Type type)
        {
            Func<TypeConverter> result;
            if (!CustomTypeConverters.TryGetValue(type, out result))
                result = () => TypeDescriptor.GetConverter(type);

            return result();
        }
    }
}