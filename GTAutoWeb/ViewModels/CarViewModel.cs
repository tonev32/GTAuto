using GTAuto.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace GTAutoWeb.ViewModel
{
    public class CarViewModel
    {

        public Guid Id { get; set; }

        [Required]
        public Guid ModelId { get; set; }
        public Model Model { get; set; }

        [Range(1950, 2100)]
        public int Year { get; set; }

        [Range(50, 2000)]
        public int HorsePower { get; set; }

        [Range(0, 10000000)]
        public decimal Price { get; set; }

        public int Mileage { get; set; }

        [Required]
        public string FuelType { get; set; }

        [Required]
        public string Transmission { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool IsReserved { get; set; } = false;

        public bool IsSold { get; set; } = false;

        public ICollection<CarFeature> CarFeatures { get; set; } = new List<CarFeature>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

