using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAuto.Data.Models
{
    public class CarFeature
    {
        public Guid CarId { get; set; }
        public Car Car { get; set; }

        public Guid FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}