
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QueryWorker.Args;
using QueryWorker.Configurations;
using QueryWorker.DataTransformers;
using QueryWorker.TransformerBuilders;

namespace QueryWorker
{
    public class DataTransformerBuildFacade<T> where T : class
    {
        private FilterBuilder<T> FilterBuilder { get; }
        private SortingBuilder<T> SortingBuilder { get; }
        private SearchBuilder<T> SearchBuilder { get; }
        private PaggingBuilder<T> PaggingBuilder { get; }

        private QueryConfiguration<T> Configuration { get; }

        public DataTransformerBuildFacade(FilterBuilder<T> filterBuilder, SortingBuilder<T> sortingBuilder, 
            SearchBuilder<T> searchBuilder, PaggingBuilder<T> paggingBuilder, ConfigurationFinder configurationFinder)
        {
            FilterBuilder = filterBuilder ?? throw new ArgumentNullException(nameof(filterBuilder));
            SortingBuilder = sortingBuilder ?? throw new ArgumentNullException(nameof(sortingBuilder));
            SearchBuilder = searchBuilder ?? throw new ArgumentNullException(nameof(searchBuilder));
            PaggingBuilder = paggingBuilder ?? throw new ArgumentNullException(nameof(paggingBuilder));
            
            Configuration = configurationFinder.Find<T>();
        }

        public DataTransformer<T> BuildFilter(FilterArgs args)
        {
            return FilterBuilder.Build(args, Configuration);
        }

        public DataTransformer<T> BuildSorting(SortingArgs args)
        {
            return SortingBuilder.Build(args, Configuration);
        }

        public DataTransformer<T> BuildSearch(SearchArgs args)
        {
            return SearchBuilder.Build(args, Configuration);
        }

        public DataTransformer<T> BuildPagging(PaggingArgs args)
        {
            return PaggingBuilder.Build(args, Configuration);
        }
    }
}
