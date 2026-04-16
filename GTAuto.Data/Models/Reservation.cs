using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTAuto.Data.Models
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public Guid CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; }

        [Required]
        public decimal DepositPaid { get; set; } 

        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;

        public DateTime ExpiryDate { get; set; }
    }
}