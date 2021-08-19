
using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QueryWorker.DataTransformers.Filters;
using QueryWorker.DataTransformers;

namespace QueryWorker.requestTransformers
{
    public class Search<T> : IDataTransformer<T> where T : class
    {
        private const string LeftBorder = "(";
        private const string RightBorder = ")";

        private readonly Expression<Func<T, string>> _propertySelector;
        public string ComparedValue { set; get; }
        public int SearchDepth { set; get; }

        public Search(Expression<Func<T, string>> propertySelector)
        {
            _propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public Search(Expression<Func<T, string>> propertySelector, string comparedValue, int searchDepth) : this(propertySelector)
        {
            ComparedValue = comparedValue ?? throw new ArgumentNullException(nameof(comparedValue));
            SearchDepth = searchDepth;
        }

        public IQueryable<T> Transform(IQueryable<T> request)
        {
            
            if (ComparedValue.StartsWith(LeftBorder) && ComparedValue.EndsWith(RightBorder)) {
                
                string comparedValue = ComparedValue.Replace(LeftBorder, string.Empty)
                    .Replace(RightBorder, string.Empty);

                return FilterRequest(request, comparedValue, FilterСomparison.Equal);
            }

            Func<string, IQueryable<T>> containsComparedValue = comparedValue
                 => FilterRequest(request, comparedValue, FilterСomparison.EqualOrMore);

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

        private IQueryable<T> FilterRequest(IQueryable<T> request, string comparedValue, FilterСomparison comparison)
        {
            var filter = new StringFilter<T>(_propertySelector, comparedValue, comparison);

            return filter.Transform(request);
        }
    }
}
