using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAuto.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}
