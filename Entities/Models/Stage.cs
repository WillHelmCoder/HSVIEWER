using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Stage
    {
        public int StageId { get; set; }
        public string StageName { get; set; }
        public string IdHsStage { get; set; }
        public DateTime Date { get; set; }
        public int TotalDealCount { get; set; }
        public string TotalDealsAmount { get; set; }
        public Int32 WorkOrderId { get; set; }
        public float Percentage { get; set; }
        public Boolean Status { get; set; }
        
        [JsonIgnore]    
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
