﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker
{
    public class QueryParams
    {
        public string[] Filter { set; get; } = new string[] { };
        public string[] Sorting { set; get; } = new string[] { };
        public string Pagging { set; get; }
       
    }
}