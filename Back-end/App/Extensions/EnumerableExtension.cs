﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }
    }
}
