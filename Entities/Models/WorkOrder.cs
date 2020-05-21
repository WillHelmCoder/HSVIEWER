using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;


namespace Entities.Models
{
    public class WorkOrder
    {
        public Int32 WorkOrderId { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public Int32 OwnerId { get; set; }
        public virtual List<Stage> Stages { get; set; }
       
        [JsonIgnore] 
        public virtual Owner Owner { get; set; }
    }
}
