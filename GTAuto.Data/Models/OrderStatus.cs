using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAuto.Data.Models
{
    public class OrderStatus
    {
        public Guid Id { get; set; }
        public int Pending { get; set; } = 0;
        public int Approved { get; set; } = 1;
        public int Cancelled { get; set; } = 2;
    }
}
