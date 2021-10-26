
using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QueryWorker.DataTransformers.Filters;
using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Filters.ExpressionCreator.String;

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

                return CreateEqualFilter(parsedValue).Transform(query);
            }

            Func<string, IQueryable<T>> containsComparedValue = comparedValue
                 => CreateContainsFilter(comparedValue).Transform(query);

            var buildedRequest = containsComparedValue(ComparedValue);
            
            var words = ComparedValue.Split();

            var substrings = new List<string>();

            foreach (string word in words)
            {
                if (word != ComparedValue)
                    buildedRequest = buildedRequest.Union(containsComparedValue(word));

                if (SearchDepth > 0)
                {
                    substrings.AddRange(word.SubstringsByLength(SearchDepth)
                        .Where(substring => substring != word));
                }        
            }
               
            foreach (string substring in substrings)
                buildedRequest = buildedRequest.Union(containsComparedValue(substring));

            return buildedRequest;
        }
        private Filter<T> CreateContainsFilter(string comparedValue)
        {
            return new Filter<T>(new StringContainsExpressionCreator<T>(PropertySelector)) 
            { 
                ComparedValue = comparedValue 
            };
        }

        private Filter<T> CreateEqualFilter(string comparedValue)
        {
            return new Filter<T>(new StringEqualExpressionCreator<T>(PropertySelector))
            {
                ComparedValue = comparedValue
            };
        }
    }
}
