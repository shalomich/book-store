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

        public event Action<string, string> Accepted;
        public event Action<string, string> Crashed;

        public void Parse(Sorting sorting)
        {
            bool isAscending;
            string propertyName;

            if (Query == null)
                return;
            
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
            if (Query == null)
                return;
            
            bool isValid = false;
            foreach (string comparisonSymbol in _symbolToFilterComparisons.Keys)
            {
                if (Query.Contains(comparisonSymbol))
                {
                    filter.FilterСomparisonValue = _symbolToFilterComparisons[comparisonSymbol];
                    isValid = true;
                    break;
                }
            }

            if (isValid == false)
            {
                Crashed?.Invoke("FilterComparison", $"Filter must be contain one of these symbols {_symbolToFilterComparisons.Keys.Aggregate((str1, str2) => $"{str1} {str2}")}");
                return;
            }

            string[] propertyAndValue = Regex.Split(Query,_filterPattern);
            string property = propertyAndValue[0];

            if (property.Contains(":") == false)
            {
                Crashed?.Invoke("Value", $"Filter must be contain ':'");
                return;
            }

            string[] propertyNameAndType = Regex.Split(property, ":");

            filter.PropertyName = propertyNameAndType[0].ToCapitalLetter();
            
            string propertyType;
            try
            {
                propertyType = propertyNameAndType[1];
            }
            catch (IndexOutOfRangeException)
            {
                Crashed?.Invoke("Value", $"Filter must be contain type of value");
                return;
            }

            if (_converters.TryGetValue(propertyType,out Func<string, object> converter) == false)
            {
                Crashed?.Invoke("Value", $"Uncorrect type {propertyType}");
                return;
            }
            
            filter.Value = (IComparable) converter(propertyAndValue[2]);

        }

        public void Parse(Pagging pagging)
        {
            if (Query == null)
                return;

            string[] numbers = Query.Split(',');

            try
            {
                pagging.PageSize = Convert.ToInt32(numbers[0]);
            }
            catch (FormatException)
            {
                Crashed?.Invoke("PageSize", "pageSize must be number");
            }

            try
            {
                pagging.PageNumber = Convert.ToInt32(numbers[1]);
            }
            catch (FormatException)
            {
                Crashed?.Invoke("PageNumber", "pageNumber must be number");
            }
            catch (IndexOutOfRangeException)
            {
                Crashed?.Invoke("PageNumber", "pageNumber is after comma");
            }
        }
    }
}
