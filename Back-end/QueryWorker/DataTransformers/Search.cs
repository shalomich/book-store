
using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QueryWorker.DataTransformers
{
    internal sealed record Search<T> : IDataTransformer<T> where T : class
    {
        private const string LeftBorder = "(";
        private const string RightBorder = ")";
        private Expression<Func<T, string>> PropertySelector { init; get; }
        public string ComparedValue { init; get; }
        public int SearchDepth { init; get; }

        public Search(Expression<Func<T, string>> propertySelector)
        {
            PropertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public IQueryable<T> Transform(IQueryable<T> query)
        {      
            if (ComparedValue.StartsWith(LeftBorder) && ComparedValue.EndsWith(RightBorder)) {
                
                string parsedValue = ComparedValue.Replace(LeftBorder, string.Empty)
                    .Replace(RightBorder, string.Empty);

                return EqualFilter(parsedValue, query);
            }

            var buildedQuery = ContainsFilter(ComparedValue, query);
            
            var words = ComparedValue.Split();

            var substrings = new List<string>();

            foreach (string word in words)
            {
                if (word != ComparedValue)
                    buildedQuery = buildedQuery.Union(ContainsFilter(word, query));

                if (SearchDepth > 0)
                {
                    substrings.AddRange(word.SubstringsByLength(SearchDepth)
                        .Where(substring => substring != word));
                }        
            }
               
            foreach (string substring in substrings)
                buildedQuery = buildedQuery.Union(ContainsFilter(substring, query));

            return buildedQuery;
        }
        private IQueryable<T> ContainsFilter(string comparedValue, IQueryable<T> query)
        {
            Expression<Func<string, bool>> comparer = value => value.Contains(comparedValue);

            return query.Where(PropertySelector.Compose(comparer));
        }

        private IQueryable<T> EqualFilter(string comparedValue, IQueryable<T> query)
        {
            Expression<Func<string, bool>> comparer = value => value == comparedValue;

            return query.Where(PropertySelector.Compose(comparer));
        }
    }
}
