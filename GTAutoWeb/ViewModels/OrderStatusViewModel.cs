namespace GTAutoWeb.ViewModel
{
    public class OrderStatusViewModel
    {
        public Guid Id { get; set; }
        public int Pending { get; set; } = 0;
        public int Approved { get; set; } = 1;
        public int Cancelled { get; set; } = 2;
    }
}
