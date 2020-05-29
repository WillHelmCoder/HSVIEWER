using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class GraphModel
    {
        public List<WorkOrderBar> OwnersGrap { get; set; }
        public List<WorkOrderBar> StagesGrap { get; set; }
    }

    public class WorkOrderBar {
        public List<string> Owners { get; set; }
        public List<int> Amounts { get;set;}
    }
}
