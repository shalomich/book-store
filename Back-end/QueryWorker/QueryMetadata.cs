using QueryWorker.Args;
using QueryWorker.DataTransformers;
using QueryWorker.DataTransformers.Paggings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryWorker
{
    public class QueryMetadata
    {
        private const string BuildingFailTemplateMessage = "Error of building {0}";

        private readonly PaggingMetadata _paggingMetadata;
        private readonly List<string> _brokenDataTransformerArgs;
        public PaggingMetadata PaggingMetadata => _paggingMetadata;
        public string[] BrokenDataTransformerArgs => _brokenDataTransformerArgs.ToArray();
        public QueryMetadata(PaggingMetadata paggingMetadata)
        {
            _paggingMetadata = paggingMetadata ?? throw new ArgumentNullException(nameof(paggingMetadata));
            _brokenDataTransformerArgs = new List<string>();
        }

        internal void AddBrokenDataTransformerArgs(IDataTransformerArgs args)
        {
            _brokenDataTransformerArgs.Add(
                string.Format(BuildingFailTemplateMessage, args.ToString()));
        }
    }
}
