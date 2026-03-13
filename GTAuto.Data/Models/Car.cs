using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GTAuto.Data.Models
{
    public class Car
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ModelId { get; set; }
        [Required]
        public Model Model { get; set; }
        [Required]

        [Range(1950, 2100)]
        public int Year { get; set; }
        [Required]
        [Range(50, 2000)]
        public int HorsePower { get; set; }
        [Required]
        [Range(0, 10000000)]
        public decimal Price { get; set; }
        [Required]
        public int Mileage { get; set; }

        [Required]
        public string FuelType { get; set; }

        [Required]
        public string Transmission { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public bool IsReserved { get; set; } = false;
        [Required]
        public bool IsSold { get; set; } = false;
        [Required]
        public bool IsAutomatic { get; set; } = false;

        public ICollection<CarFeature> CarFeatures { get; set; } = new List<CarFeature>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
