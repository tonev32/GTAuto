using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTAuto.Data.Models
{
    public class CarImage
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ImagePath { get; set; } 

        [Required]
        public int Order { get; set; } 

        [Required]
        public Guid CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; }
    }
}