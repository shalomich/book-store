
using QueryWorker.Extensions;
using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Filters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using QueryWorker.Args;
using System.Linq;

namespace QueryWorker.Configurations
{
    public abstract class QueryConfiguration<TClass> where TClass : class
    {
        private readonly Dictionary<string, Sorting<TClass>> _sortings = new Dictionary<string, Sorting<TClass>>();
        private readonly Dictionary<string, StringFilter<TClass>> _stringFilters = new Dictionary<string, StringFilter<TClass>>();
        private readonly Dictionary<string, NumberFilter<TClass>> _numberFilters = new Dictionary<string, NumberFilter<TClass>>();
        private readonly Dictionary<string, CollectionFilter<TClass>> _collectionFilters = new Dictionary<string, CollectionFilter<TClass>>();

        private readonly Dictionary<string, IDataTransformer<TClass>> _filters = new Dictionary<string, IDataTransformer<TClass>>();

        private readonly IMapper _mapper; 

        protected QueryConfiguration()
        {
            var mapperConfig = new MapperConfiguration(builder =>
            {
                builder.CreateMap<SortingArgs, Sorting<TClass>>();
                builder.CreateMap<FilterArgs, StringFilter<TClass>>();
                builder.CreateMap<FilterArgs, NumberFilter<TClass>>()
                    .ForMember(filter => filter.ComparedValue, mapper =>
                        mapper.MapFrom(args => double.Parse(args.ComparedValue)));
                builder.CreateMap<FilterArgs, CollectionFilter<TClass>>()
                    .ForMember(filter => filter.ComparedValue, mapper =>
                        mapper.MapFrom(args => args.ComparedValue.Split(',', StringSplitOptions.None)));
            });

            _mapper = new Mapper(mapperConfig);
        }

        internal IDataTransformer<TClass> BuildTransformer(IDataTransformerArgs args) => args switch
        {
            SortingArgs sortingArgs => _mapper.Map(sortingArgs, _sortings[args.PropertyName]),
            FilterArgs filterArgs => _mapper.Map(filterArgs, _filters[args.PropertyName]),
            _ => throw new ArgumentException()
        };

        protected void CreateSorting(string propertyKey,Expression<Func<TClass,object>> propertySelector)
        {
            var sorting = new Sorting<TClass>(propertySelector);

            _sortings.Add(propertyKey, sorting);
        }

        protected void CreateFilter(string propertyKey,Expression<Func<TClass, string>> propertySelector)
        {
            _filters.Add(propertyKey, new StringFilter<TClass>(propertySelector));
        }

        protected void CreateFilter(string propertyKey,Expression<Func<TClass, double>> propertySelector)
        {
            _filters.Add(propertyKey, new NumberFilter<TClass>(propertySelector));
        }

        protected void CreateFilter(string propertyKey, Expression<Func<TClass, IEnumerable<string>>> propertySelector)
        {
            _filters.Add(propertyKey, new CollectionFilter<TClass>(propertySelector));
        }
    }
}
