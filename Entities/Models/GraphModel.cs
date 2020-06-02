using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class GraphModel
    {
        public List<WorkOrderBar> OwnersGrap { get; set; } = new List<WorkOrderBar>();
        public List<WorkOrderBar> StagesGrap { get; set; } = new List<WorkOrderBar>();
    }

    public class WorkOrderBar {
        public List<string> Owners { get; set; }
        public List<int> Amounts { get;set;}
    }
}
