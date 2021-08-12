using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Extensions
{
    public static class MapperExtension
    {
        public static Type GetSourceType(this IMapper mapper, Type destinationType)
        {
            return mapper.ConfigurationProvider
                .GetAllTypeMaps()
                .Single(mapper => mapper.DestinationType == destinationType)
                .SourceType;
        }
    }
}
