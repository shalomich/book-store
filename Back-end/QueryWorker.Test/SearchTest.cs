using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QueryWorker.Test
{
    /*
    public class SearchTest
    {
        public class Author
        {
            public string Name { get; set; }
        }

        [Theory]
        [MemberData(nameof(GetTransformData))]
        public void TransformTest(Author[] input, string value, int depth, Author[] output)
        {
            var _search = new Search<Author>(e => e.Name) { ComparedValue = value, SearchDepth = depth};

            var result = _search.Transform(input.AsQueryable()).ToArray();

            Assert.True(output.ToHashSet().SetEquals(result));

        }

        public static IEnumerable<object[]> GetTransformData()
        {
            var authors = new Author[]
            {
                new() {Name = "Джек Лондон"},
                new() {Name = "БоДжек Лондоний"},
                new() {Name = "Джек"},
                new() {Name = "Воробей Джек"},
                new() {Name = "Джеф Робен"},
                new() {Name = "Джон Гамжек"},
                new() {Name = "Шрек"},
                new() {Name = "женя Гондон"}
            };

            yield return new object[] { authors, "Джек Лондон", 7,
                new Author[] { authors[0], authors[1], authors[2], authors[3] } };
            yield return new object[] { authors, "Джек", 2, authors };
            yield return new object[] { authors, "Джек", 3,
                new Author[] { authors[0], authors[1], authors[2], authors[3],authors[4],authors[5]}};
            yield return new object[] { authors, "(Джек Лондон)", 1, new Author[] { authors[0] } };
           
            yield return new object[] { authors, "Джек", 4, 
                new Author[] { authors[0], authors[1], authors[2], authors[3]} };
            yield return new object[] { authors, "(Джэк Лондон)", 1, new Author[] {} };
            yield return new object[] { authors, "Джек Лондон", 5, 
                new Author[] { authors[0], authors[1], authors[2], authors[3],authors[7]} };

        }
    }*/
}