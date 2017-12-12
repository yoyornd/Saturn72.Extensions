﻿
using System;
using System.Linq;
using System.Reflection;

namespace Saturn72.Extensions
{
    public static class TypeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type,
            Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var att = type.GetTypeInfo().GetCustomAttributes(
                typeof (TAttribute), true
                ).FirstOrDefault() as TAttribute;
            return att != null ? valueSelector(att) : default(TValue);
        }
    }
}
