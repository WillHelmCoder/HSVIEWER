using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Pipeline
    {
        public Int32 PipelineId { get; set; }
        public string Name { get; set; }
        public Int32 WorkOrderId { get; set; }
        public string HsPipeLineId { get; set; }
        [JsonIgnore]
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
