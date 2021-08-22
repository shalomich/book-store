using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Common.ViewModels
{
    public record ValidQueryData<T> (T Data, string[] Errors);
}
