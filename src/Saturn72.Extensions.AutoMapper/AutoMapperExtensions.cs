using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Saturn72.Extensions
{
    public static class AutoMapperExtensions
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> sourceList)
        {
            return sourceList.Select(item => Map<TSource, TDestination>(item));
        }
    }
}