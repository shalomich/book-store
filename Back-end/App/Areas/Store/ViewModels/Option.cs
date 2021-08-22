using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.ViewModels
{
    public record Option
    {
        public string Text { init; get; }
        public string Value { init; get; }
    }
}
