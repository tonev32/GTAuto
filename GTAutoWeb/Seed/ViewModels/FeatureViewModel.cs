using GTAuto.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace GTAutoWeb.ViewModel
{
    public class FeatureViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<CarFeature> CarFeatures { get; set; } = new List<CarFeature>();
    }
}
