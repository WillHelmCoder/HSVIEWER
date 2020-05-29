using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OwnerAnalysis
    {
        public Int32 OwnerAnalysisId { get; set; }
        public string OwnerName { get; set; }
        public Int32 DealsNumber { get; set; }
        public double DealAverage { get; set; }
        public double OwnerPipelineValue { get; set; }
        public Int32 WorkOrderId { get; set; }
    }
}
