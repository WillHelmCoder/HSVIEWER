using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class HsOwner
    {
        public Int32 HsOwnerId { get; set; }
        public string eMail { get; set; }
        public string Name { get; set; }
        public Int32 WorkOrderId { get; set; }
        public string HsId { get; set; }
    }
}
