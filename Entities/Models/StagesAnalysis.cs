using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class StagesAnalysis
    {
        public Int32 StagesAnalysisId { get; set; }
        public string Stagename { get; set; }
        public Int32 DealsNumber { get; set; }
        public double DealAverage { get; set; }
        public double StageValue { get; set; }
    }
}
