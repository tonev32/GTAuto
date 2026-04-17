using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAuto.Data.Models
{
    public class WishlistCar
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!; 
        [Required]
        public Guid CarId { get; set; } 

        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; } = null!;
    }
}
