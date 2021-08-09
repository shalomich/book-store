using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using QueryWorker.Visitors;
using System.Text.RegularExpressions;
using App.Entities;

namespace QueryWorker.Test
{
    public class QueryTransformerTest
    {
        private static readonly IConfiguration _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>() {
                    {"query:pagging:MaxPageSize","60"},
                    {"query:sorting:Publication:SortedProperties:0", "Name"},
                    {"query:sorting:Publication:SortedProperties:1", "Cost"},
                    {"query:filter:Publication:FilteredProperties:0","Name"},
                    {"query:filter:Publication:FilteredProperties:1","Cost"}
                })
                .Build();
        private QueryTransformer<Publication> _transformer = new QueryTransformer<Publication>(_configuration, new QueryParser());

        [Theory]
        [MemberData(nameof(GetCorrectTransformData))]
        public void CorrectTransform(IQueryable<Publication> publications, QueryParams parameters, Publication[] expected)
        {
            var result = _transformer.Transform(publications, parameters).ToList();

            Assert.True(expected.SequenceEqual(result));
        }

        public static IEnumerable<object[]> GetCorrectTransformData()
        {
            var p1 = new Publication { Name = "1", Cost = 4};
            var p2 = new Publication { Name = "1", Cost = 4};
            var p3 = new Publication { Name = "2", Cost = 3};
            var p4 = new Publication { Name = "2", Cost = 2};
            var p5 = new Publication { Name = "3", Cost = 2};
            var p6 = new Publication { Name = "3", Cost = 1};
            var p7 = new Publication { Name = "3", Cost = 1};
            var p8 = new Publication { Name = "4", Cost = 1};

            var source = new List<Publication> { p3, p4, p1, p2, p8, p7, p5, p6 }.AsQueryable();
            yield return new object[] {source, new QueryParams
            {
                Pagging = "2,1",
                Filter = new string[] { "name:s=3" },
                Sorting = new string[] {"cost"}
            }, new Publication[]{p7,p6} };
            yield return new object[] {source, new QueryParams
            {
                Pagging = "4,1",
                Filter = new string[]{"cost:i>2"},
                Sorting = new string[] {"name","cost"}
            }, new Publication[]{p1,p2,p3} };
        } 
    }
}
