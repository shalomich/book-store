using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace QueryWorker.Test
{
    /*
    public class PaggingTest
    {
        
        [Theory]
        [MemberData(nameof(GetCorrectPaggingData))]
        public void CorrectPagging(int pageSize, int pageNumber, IEnumerable<int> source, IEnumerable<int> expected)
        {
            var pagging = new Pagging<int>(pageSize, pageNumber);

            var paggedData = pagging.Execute(source.AsQueryable()).ToList();

            Assert.True(paggedData.SequenceEqual(expected));
        }

        public static IEnumerable<object[]> GetCorrectPaggingData()
        {
            var source = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            yield return new object[] { 2, 2, source, new int[] { 3, 4 } };
            yield return new object[] { 4, 3, source, new int[] { 9 } };
            yield return new object[] { 10, 1, source, source };
            yield return new object[] { 3, 4, source, new int[] { } };
        }
    }*/
}
