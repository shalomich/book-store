
using QueryWorker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QueryWorker.DataTransformers.Filters;
using QueryWorker.DataTransformers;

namespace QueryWorker.DataTransformers
{
    public class Search<T> : DataTransformer<T> where T : class
    {
        private const string LeftBorder = "(";
        private const string RightBorder = ")";
        private Expression<Func<T, string>> PropertySelector { init; get; }
        public string ComparedValue { set; get; }
        public int SearchDepth { set; get; }

        public Search()
        {

        }
        public Search(Expression<Func<T, string>> propertySelector)
        {
            PropertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public override IQueryable<T> Transform(IQueryable<T> request)
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
            var filter = new StringFilter<T>(PropertySelector) { ComparedValue = comparedValue, Comparison = comparison};

            return filter.Transform(request);
        }
    }
}
