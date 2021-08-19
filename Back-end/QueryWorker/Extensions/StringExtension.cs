using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Extensions
{
    public static class StringExtension
    {
        public static string[] SubstringsByLength(this string str, int substringLength)
        {
            int startIndex = 0;

            if (substringLength <= 0)
                throw new ArgumentOutOfRangeException();

            var substrings = new List<string>();

            while(startIndex <= str.Length - substringLength)
            {
                var substring = str.Substring(startIndex, substringLength);
                substrings.Add(substring);
                startIndex++;
            }

            return substrings.ToArray();
        }
    }
}
