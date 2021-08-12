using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Storage.ViewModels.Identities
{
    public record EntityIdentity
    {
        public int Id { init; get; }
        public string Name { init; get; }
    }
}
