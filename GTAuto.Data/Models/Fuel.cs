using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAuto.Data.Models
{
    public class Fuel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string FuelConsumption { get; set; }
    }
}
