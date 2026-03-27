using GTAuto.Data.Models;

namespace GTAutoWeb.ViewModel
{
    public class CarFeatureViewModel
    {
        public Guid CarId { get; set; }
        public Car Car { get; set; }

        public Guid FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
