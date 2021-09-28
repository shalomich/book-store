﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Extensions
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

        public static Type GetDestinationType(this IMapper mapper, Type sourceType, Type destinationBaseType)
        {
            return mapper.ConfigurationProvider
                .GetAllTypeMaps()
                .Single(mapper => mapper.SourceType == sourceType
                    && mapper.DestinationType.IsSubclassOf(destinationBaseType))
                .DestinationType;
        }
    }
}