using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAuto.Data.Models
{
    public class Brand
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
