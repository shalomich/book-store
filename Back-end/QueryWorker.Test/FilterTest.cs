using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Storage.Models;

namespace QueryWorker.Test
{
    public class FilterTest
    {
        
        [Theory]
        [MemberData(nameof(GetCorrectFilterData))]
        public void CorrectFilter(string propertyName, IComparable value, FilterСomparison comparison, IEnumerable<Publication> source, IEnumerable<Publication> expected)
        {
            var filter = new Filter(propertyName, value, comparison);

            var fileterdData = filter.Execute(source.AsQueryable());

            Assert.True(fileterdData.SequenceEqual(expected));
        }

        public static IEnumerable<object[]> GetCorrectFilterData()
        {

            var p1 = new Publication { ReleaseYear = 2009, Cost = 3, Name = "2" };
            var p2 = new Publication { ReleaseYear = 2018, Cost = 4, Name = "1" };
            var p3 = new Publication { ReleaseYear = 2003, Cost = 5, Name = "1" };
            var p4 = new Publication { ReleaseYear = 2020, Cost = 6, Name = "2" };
            var p5 = new Publication { ReleaseYear = 2010, Cost = 7, Name = "2" };

            var allPublications = new Publication[] { p1, p2, p3, p4, p5 };

            yield return new object[] { "ReleaseYear", 2012, FilterСomparison.More, allPublications, new Publication[] { p2, p4 } };

            yield return new object[] { "Cost", 2, FilterСomparison.Less, allPublications, new Publication[] { } };

            yield return new object[] { "Name", "2", FilterСomparison.Equal, allPublications, new Publication[] { p1, p4, p5 } };
        }

        [Theory]
        [InlineData("qwerty")]
        [InlineData("releaseYear")]
        [InlineData("_ISBNMask")]
        public void SetUncorrectPropertyName(string propertyName)
        {
            var filter = new Filter { PropertyName = propertyName };

            Assert.Throws<ArgumentException>(() => filter.Execute<Publication>(null));
        }

        [Theory]
        [InlineData("Type", 1)]
        [InlineData("AddingDate", "qwerty")]
        public void SetUncorrectValue(string propertyName, IComparable value)
        {
            var filter = new Filter { PropertyName = propertyName, Value = value };

            Assert.Throws<ArgumentException>(() => filter.Execute<Publication>(null));
        }

        [Theory]
        [InlineData("Name",new string[] { "Cost"})]
        [InlineData("Cost", new string[] { "Name"})]
        [InlineData("AddingDate", new string[] { })]
        public void SetLimitPropertyName(string propertyName, string[] filteredProperties)
        {
            var filter = new Filter { PropertyName = propertyName, FilteredProperties = filteredProperties };
            Assert.Throws<ArgumentException>(() => filter.Execute<Publication>(null));
        }
    }
}