namespace ASPNETapp2.Models
{
    public class UpdateOrderPrice
    {
        public int OrderId { get; set; }
        public double NewPrice { get; set; }

        public UpdateOrderPrice() { }
    }
}