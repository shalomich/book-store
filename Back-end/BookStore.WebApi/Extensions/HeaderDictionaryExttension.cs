using Microsoft.AspNetCore.Http;
using QueryWorker.DataTransformers.Paggings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Extensions
{
    public static class HeaderDictionaryExttension
    {
        public static void Add(this IHeaderDictionary headers, PaggingMetadata metadata)
        {
            headers.Add(nameof(metadata.PageSize), metadata.PageSize.ToString());
            headers.Add(nameof(metadata.PageNumber), metadata.PageNumber.ToString());
            headers.Add(nameof(metadata.DataCount), metadata.DataCount.ToString());
            headers.Add(nameof(metadata.CurrentPageDataCount), metadata.CurrentPageDataCount.ToString());
            headers.Add(nameof(metadata.PageCount), metadata.PageCount.ToString());
        }
    }
}
