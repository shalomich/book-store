using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using QueryWorker.Extensions;

namespace QueryWorker.Test
{
    public class StringExtensionTest
    {
        [Theory]
        [MemberData(nameof(GetSubstringsByLengthTestData))]
        public void SubstringsByLengthTest(string str, int substringLength, string[] substrings)
        {
            string[] result = str.SubstringsByLength(substringLength);

            Assert.True(substrings.SequenceEqual(result));
        }

        public static IEnumerable<object[]> GetSubstringsByLengthTestData()
        {
            yield return new object[] { "12345", 2, new string[] { "12", "23","34","45"} };
            yield return new object[] { "12345", 4, new string[] { "1234", "2345" } };
            yield return new object[] { "12345", 5, new string[] { "12345" } };
            yield return new object[] { "12345", 6, new string[] { } };
        }
    }
}
