using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;
using System.Threading.Tasks;
using System.IO.Pipelines;

namespace Entities.Models
{
    public class Stage
    {
     
        public Int32 StageId { get; set; }
        public Int32 WorkOrderId { get; set; }
        public string HsStageId { get; set; }
        public string StageName { get; set; }   
        public string Pipeline { get; set; }   
        public string HsPipelineId { get; set; } 
        public float Forecast { get; set; }
     
    } 
}
