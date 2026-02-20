using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAuto.Data.Models
{
    public class Model
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
