using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAuto.Data.Models
{
    public class Car
    {
        public Guid Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }


        public bool IsAvailable { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<CarFeature> CarFeatures { get; set; }
    }
}
