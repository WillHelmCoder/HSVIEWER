using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace Entities.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string SalesGoal { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public Boolean Isprocessing { get; set; }

        [JsonIgnore]

        public List<WorkOrder> WorkOrders { get; set; }

    }
}
