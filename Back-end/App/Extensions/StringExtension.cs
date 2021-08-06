using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Extensions
{
    public static class StringExtension
    {
        public static string ToLowFirstLetter(this string str)
        {
            return str.Substring(0, 1).ToLower() + (str.Length > 1 ? str.Substring(1) : "");
        }
    }
}
