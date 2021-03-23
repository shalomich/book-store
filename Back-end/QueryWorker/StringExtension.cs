using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker
{
    public static class StringExtension
    {
        public static string ToCapitalLetter(this string str)
        {
            return str.Substring(0, 1).ToUpper() + (str.Length > 1 ? str.Substring(1) : "");
        }
    }
}
