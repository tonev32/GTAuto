using GTAuto.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace GTAutoWeb.ViewModel
{
    public class ModelViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
