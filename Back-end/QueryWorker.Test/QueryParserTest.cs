using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace QueryWorker.Test
{
    /*
    public class QueryParserTest
    {

        private QueryParser _parser = new QueryParser();

        [Theory]
        [InlineData("5,2",5,2)]
        [InlineData("3,4", 3, 4)]
        [InlineData("2,5", 2, 5)]
        public void CorrectParsePagging(string query, int pageSize, int pageNumber)
        {
            _parser.Query = query;

            var pagging = new Pagging();
            pagging.Accept(_parser);
            
            Assert.Equal(pageSize, pagging.PageSize);
            Assert.Equal(pageNumber, pagging.PageNumber);
        }

        [Theory]
        [InlineData("Name","Name",true)]
        [InlineData("name", "Name", true)]
        [InlineData("-cost", "Cost", false)]
        public void CorrectParseSorting(string query, string propertyName, bool isAscending)
        {
            _parser.Query = query;

            var sorting = new Sorting();
            sorting.Accept(_parser);

            Assert.Equal(propertyName, sorting.PropertyName);
            Assert.Equal(isAscending, sorting.IsAscending);
        }

        [Theory]
        [InlineData("Name:s=noname","Name","noname",FilterСomparison.Equal)]
        [InlineData("Cost:i<5", "Cost", 5, FilterСomparison.Less)]
        [InlineData("cost:i>10", "Cost", 10, FilterСomparison.More)]

        public void CorrectParseFilter(string query, string propertyName, object value, FilterСomparison сomparison)
        {
            _parser.Query = query;

            var filter = new Filter();
            filter.Accept(_parser);

            Assert.Equal(propertyName, filter.PropertyName);
            Assert.Equal(value, filter.Value);
            Assert.Equal(сomparison, filter.FilterСomparisonValue);
        }

    }*/
}
