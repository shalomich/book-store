using System;
using System.Collections.Generic;
using System.Text;
using QueryWorker;
using System.Linq;
using Xunit;
using App.Entities;

namespace QueryWorker.Test
{
    public class SortingTest
    {
        [Theory]
        [MemberData(nameof(GetCorrectSortData))]
        public void CorrectSort(string propertyName, bool isAscending, IEnumerable<Publication> source, IEnumerable<Publication> expected)
        {
            var sorting = new Sorting(propertyName, isAscending);

            var sortedData = sorting.Execute(source.AsQueryable()).ToList();

            Assert.True(sortedData.SequenceEqual(expected));
        }

        public static IEnumerable<object[]> GetCorrectSortData()
        {
            var p1 = new Publication { ReleaseYear = 2009, Cost = 6, Type = "Манга" };
            var p2 = new Publication { ReleaseYear = 2018, Cost = 2, Type = "Ранобэ" };
            var p3 = new Publication { ReleaseYear = 2003, Cost = 1, Type = "Книга" };
            var p4 = new Publication { ReleaseYear = 2020, Cost = 4, Type = "Графический роман" };
            var p5 = new Publication { ReleaseYear = 2010, Cost = 5, Type = "Артбук" };

            var source = new Publication[] { p1, p2, p3, p4, p5 };

            yield return new object[] { "ReleaseYear", true, source, new Publication[] { p3, p1, p5, p2, p4 } };
            yield return new object[] { "Cost", false, source, new Publication[] { p1, p5, p4, p2, p3 } };
            yield return new object[] { "Type", true, source, new Publication[] { p5, p4, p3, p1, p2 } };
        }

        [Theory]
        [InlineData("qwerty")]
        [InlineData("releaseYeat")]
        [InlineData("_ISBNMask")]
        public void SetUncorrectPropertyName(string propertyName)
        {
            var sorting = new Sorting() { PropertyName = propertyName};

            Assert.Throws<ArgumentException>(() => sorting.Execute<Publication>(null));
        }

        [Theory]
        [InlineData("Name", new string[] { "Cost"})]
        [InlineData("ReleaseYear", new string[] { })]
        [InlineData("ISBN", new string[] {"Name","Cost"})]
        public void SetLimitPropertyName(string propertyName, string[] sortedProperties)
        {
            var sorting = new Sorting() { PropertyName = propertyName, SortedProperties = sortedProperties };

            Assert.Throws<ArgumentException>(() => sorting.Execute<Publication>(null));
        }
    }
}