using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker.Args
{
    public record SortingArgs : DataTransformerArgs
    {
        [Required]
        public string PropertyName { init; get; }
        public bool IsAscending { init; get; } = true;

        public override string ToString()
        {
            return $"Sorting(propertyName: {PropertyName}," +
                $"isAscending: {IsAscending})";
        }
    }
}
