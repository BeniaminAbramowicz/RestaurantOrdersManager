namespace ASPNETapp2.Models
{
    public class OrderItem
    {
        public Meal Meal { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public OrderItem() { }
        public OrderItem(Meal meal, int quantity, double price)
        {
            Meal = meal;
            Quantity = quantity;
            Price = price;
        }
        public OrderItem(Meal meal, double price)
        {
            Meal = meal;
            Price = price;
        }
    }
}