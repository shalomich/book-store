
using QueryWorker.DataTransformers.Filters;
using System.ComponentModel.DataAnnotations;


namespace QueryWorker.Args
{
    public record FilterArgs : IDataTransformerArgs
    {
        [Required]
        public string PropertyName { init; get; }
        
        [Required]
        public string ComparedValue { init; get; }

        public FilterСomparison Comparison { init; get; } = FilterСomparison.Equal;

        public override string ToString()
        {
            return $"Filter(propertyName: {PropertyName}, " +
                $"comparedValue: {ComparedValue}," +
                $"comparison: {Comparison})";
        }
    }
}
