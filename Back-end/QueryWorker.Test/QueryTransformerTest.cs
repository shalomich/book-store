using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using System.Text.RegularExpressions;

namespace QueryWorker.Test
{
    /*
    public class DataTransformerFacadeTest
    {

        private DataTransformerFacade _transformer = new DataTransformerFacade(
            typeof(DataTransformerFacadeTest).GetType().Assembly);

       

        [Theory]
        [MemberData(nameof(GetCorrectTransformData))]
        public void CorrectTransform(IQueryable<Product> Products, QueryParams parameters, Product[] expected)
        {
            var result = _transformer.Transform(Products, parameters).ToList();

            Assert.True(expected.SequenceEqual(result));

        }

        public static IEnumerable<object[]> GetCorrectTransformData()
        {
            var p1 = new Product { Name = "1", Cost = 4};
            var p2 = new Product { Name = "1", Cost = 4};
            var p3 = new Product { Name = "2", Cost = 3};
            var p4 = new Product { Name = "2", Cost = 2};
            var p5 = new Product { Name = "3", Cost = 2};
            var p6 = new Product { Name = "3", Cost = 1};
            var p7 = new Product { Name = "3", Cost = 1};
            var p8 = new Product { Name = "4", Cost = 1};

            var source = new List<Product> { p3, p4, p1, p2, p8, p7, p5, p6 }.AsQueryable();
            yield return new object[] {source, new QueryParams
            {
                Pagging = "2,1",
                Filter = new string[] { "name:s=3" },
                Sorting = new string[] {"cost"}
            }, new Product[]{p7,p6} };
            yield return new object[] {source, new QueryParams
            {
                Pagging = "4,1",
                Filter = new string[]{"cost:i>2"},
                Sorting = new string[] {"name","cost"}
            }, new Product[]{p1,p2,p3} };
        } 
    }*/
}
