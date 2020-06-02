using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class MainGraphModel
    {
        public List<OwnerGraphModel> OwnersGraphs { get; set; } = new List<OwnerGraphModel>();
        public List<WorkOrderBar> StagesGraph { get; set; } = new List<WorkOrderBar>();
        public List<string> WorkOrders { get; set; } = new List<string>();
    }
    public class OwnerGraphModel
    {
        public string Name { get; set; }
        public List<WorkOrderBar> OwnersGrap { get; set; } = new List<WorkOrderBar>();
    }

    public class WorkOrderBar
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
    }

    public class PreWorkOrderBar
    {
        public string Label { get; set; }
        public int Data { get; set; }
    }
}
