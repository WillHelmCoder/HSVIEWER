using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OwnerStageAnalysis
    {
        public Int32 OwnerStageAnalysisId { get; set; }
        public string OwnerName { get; set; }
        public string StageName { get; set; }
        public Int32 DealsNumber { get; set; }
        public double DealAverage { get; set; }
        public double OwnerPipelineValue { get; set; }

    }
}
