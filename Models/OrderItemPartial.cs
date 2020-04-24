namespace ASPNETapp2.Models
{
    public class OrderItemPartial
    {
        public int OrderItemId { get; set; }
        public string MealName { get; set; }
        public int Quantity { get; set; }

        public OrderItemPartial() { }
    }
}