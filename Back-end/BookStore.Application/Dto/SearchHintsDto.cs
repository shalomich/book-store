using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record SearchHintsDto
    {
        public string[] Books { init; get; }
        public string[] Authors { init; get; }
        public string[] Publishers { init; get; }
    }
}
