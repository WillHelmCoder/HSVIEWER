using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Deal
    {
        public int DealId { get; set; }       
        public string  Amount { get; set; }
        public string Name { get; set; }
        public string HsStageId { get; set; }
        public string HsOwnerId { get; set; }
        //public Int32 StageId { get; set; }
        //public Int32 OwnerId { get; set; }
        //public virtual Stage Stage { get; set; }
        public string hsdate { get; set; }
    }
}
