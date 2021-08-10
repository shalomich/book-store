using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Extensions
{
    public static class MapperExtension
    {
        public static Type GetDestinationType(this IMapper mapper, Type sourceType)
        {
            return mapper.ConfigurationProvider
                .GetAllTypeMaps()
                .Single(mapper => mapper.SourceType == sourceType)
                .DestinationType;
        }
    }
}
