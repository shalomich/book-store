using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace QueryWorker.Visitors
{
    public class QueryParser : IQueryParser
    {
        public string Query { set; get; }

        private string _filterPattern = "(=)|(<)|(>)";

        private Dictionary<string, FilterСomparison> _symbolToFilterComparisons = new Dictionary<string, FilterСomparison>()
        {
            { "=", FilterСomparison.Equal },
            { "<", FilterСomparison.Less},
            { ">", FilterСomparison.More }
        };

        private Dictionary<string, Func<string, object>> _converters = new Dictionary<string, Func<string, object>>()
        {
            {"i", str => Convert.ToInt32(str)},
            {"d", str => Convert.ToDouble(str)},
            {"b", str => Convert.ToBoolean(str)},
            {"s", str => str},
            {"t", str => Convert.ToDateTime(str)},
        };

        public void Parse(Sorting sorting)
        {
            bool isAscending;
            string propertyName;
            
            if (Query.StartsWith('-'))
            {
                isAscending = false;
                propertyName = Query.TrimStart(new char[] { '-'}).ToCapitalLetter();
            }
            else
            {
                isAscending = true;
                propertyName = Query.ToCapitalLetter();
            }
             
            sorting.PropertyName = propertyName;
            sorting.IsAscending = isAscending;
        }

        public void Parse(Filter filter)
        {            
            string[] propertyAndValue = Regex.Split(Query,_filterPattern);
                
            string[] propertyAndType = Regex.Split(propertyAndValue[0], ":");

            var propertyName = propertyAndType[0].ToCapitalLetter();
            var propertyType = propertyAndType[1];
                
            var comparisonSymbol = propertyAndValue[1];
            FilterСomparison filterСomparisonValue = _symbolToFilterComparisons[comparisonSymbol];

            Func<string, object> converter = _converters[propertyType];
            IComparable value = (IComparable) converter(propertyAndValue[2]);

            filter.PropertyName = propertyName;
            filter.Value = value;
            filter.FilterСomparisonValue = filterСomparisonValue;
        }

        public void Parse(Pagging pagging)
        {
            int[] numbers = Query.Split(',').Select(str => int.Parse(str)).ToArray();

            pagging.PageSize = numbers[0];
            pagging.PageNumber = numbers[1];
        }
    }
}
