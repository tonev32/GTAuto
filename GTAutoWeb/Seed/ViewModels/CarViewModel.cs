using GTAuto.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GTAutoWeb.ViewModel
{
    public class CarViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ModelId { get; set; }

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
        [Display(Name = "Front Image")]
        public IFormFile? FrontImage { get; set; }

        [Display(Name = "Back Image")]
        public IFormFile? BackImage { get; set; }

        [Display(Name = "Interior Image")]
        public IFormFile? InteriorImage { get; set; }

        public bool IsReserved { get; set; } = false;
        public bool IsSold { get; set; } = false;
        public bool IsAutomatic { get; set; } = false;
        public bool IsFlashOffer { get; set; } = false;

        public ICollection<CarFeature> CarFeatures { get; set; } = new List<CarFeature>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}