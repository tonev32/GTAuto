using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAuto.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        public Guid CarId { get; set; }
        public Car Car { get; set; }

        public DateTime CreatedOn { get; set; }

        [Range(0, 1000000)]
        public decimal DepositAmount { get; set; }

        public OrderStatus Status { get; set; }
    }
}
