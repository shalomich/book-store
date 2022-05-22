using AutoMapper;
using AutoMapper.Internal;
using System;
using System.Linq;

namespace BookStore.Application.Extensions;
public static class MapperExtension
{
    public static Type GetDestinationType(this IMapper mapper, Type sourceType, Type destinationBaseType)
    {
        return mapper
            .ConfigurationProvider
            .Internal()
            .GetAllTypeMaps()
            .Single(mapper => mapper.SourceType == sourceType
                && mapper.DestinationType.IsSubclassOf(destinationBaseType))
            .DestinationType;
    }
}
