using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Various;

namespace QueryWorker.Visitors
{
    public class QueryParser : IQueryParser
    {
        private static readonly string _notExistTypeSymbolMessage;
        private static readonly string _mustHaveComparisonSymbolMessage;
        private static readonly string _mustHaveСolonMessage;
        private static readonly string _mustHaveTypeSymbolMessage;
        private static readonly string _mustHavePageSizeMessage;
        private static readonly string _mustHavePageNumberMessage;
        private static readonly string _mustHavePageNumberAfterCommaMessage;

        private const string _filterPattern = "(=)|(<)|(>)";

        private static readonly Dictionary<string, FilterСomparison> _symbolToFilterComparisons = new Dictionary<string, FilterСomparison>()
        {
            { "=", FilterСomparison.Equal },
            { "<", FilterСomparison.Less},
            { ">", FilterСomparison.More }
        };

        private static readonly Dictionary<string, Func<string, object>> _converters = new Dictionary<string, Func<string, object>>()
        {
            {"i", str => Convert.ToInt32(str)},
            {"d", str => Convert.ToDouble(str)},
            {"b", str => Convert.ToBoolean(str)},
            {"s", str => str},
            {"t", str => Convert.ToDateTime(str)},
        };

        public event Action<string, string, IInformed> Accepted;
        public event Action<string, string, IInformed> Crashed;

        public string Query { set; get; }

        static QueryParser()
        {
            string typeSymbols = _converters.Select(converter => converter.Key).Aggregate((str1, str2) => $"{str1} {str2}");
            _notExistTypeSymbolMessage = ExceptionMessages
                .GetMessage(ExceptionMessageType.NotExist,"PropertyType",typeSymbols);

            _mustHaveTypeSymbolMessage = ExceptionMessages
                .GetMessage(ExceptionMessageType.MustHave, "Filter", typeSymbols);

            IEnumerable<string> comparisonSymbols = _symbolToFilterComparisons.Select(comparison => comparison.Key);
            _mustHaveComparisonSymbolMessage = ExceptionMessages
                .GetMessage(ExceptionMessageType.MustHave, "Filter", comparisonSymbols.Aggregate((str1, str2) => $"{str1} {str2}"));

            _mustHaveСolonMessage = ExceptionMessages
                .GetMessage(ExceptionMessageType.MustHave, "Filter", ":");
            
            _mustHavePageSizeMessage = ExceptionMessages
                .GetMessage(ExceptionMessageType.MustHave, "PageSize", "number");

            _mustHavePageNumberMessage = ExceptionMessages
                .GetMessage(ExceptionMessageType.MustHave, "PageNumber", "number");

            _mustHavePageNumberMessage = ExceptionMessages
                .GetMessage(ExceptionMessageType.MustHave, "PageNumber", "number after comma");
        }
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
                Crashed?.Invoke("FilterComparison", _mustHaveComparisonSymbolMessage,this);
                return;
            }

            string[] propertyAndValue = Regex.Split(Query,_filterPattern);
            string property = propertyAndValue[0];

            if (property.Contains(":") == false)
            {
                Crashed?.Invoke("Value", _mustHaveСolonMessage,this);
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
                Crashed?.Invoke("Value", _mustHaveTypeSymbolMessage,this);
                return;
            }

            if (_converters.TryGetValue(propertyType,out Func<string, object> converter) == false)
            {
                Crashed?.Invoke("Value", _notExistTypeSymbolMessage,this);
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
                Crashed?.Invoke("PageSize", _mustHavePageSizeMessage,this);
            }

            try
            {
                pagging.PageNumber = Convert.ToInt32(numbers[1]);
            }
            catch (FormatException)
            {
                Crashed?.Invoke("PageNumber", _mustHavePageNumberMessage,this);
            }
            catch (IndexOutOfRangeException)
            {
                Crashed?.Invoke("PageNumber", _mustHavePageNumberAfterCommaMessage,this);
            }
        }

        public override string ToString()
        {
            return "QueryParser";
        }
    }
}
