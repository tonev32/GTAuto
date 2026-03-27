using GTAuto.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace GTAutoWeb.ViewModel
{
    public class OrderViewModel
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
