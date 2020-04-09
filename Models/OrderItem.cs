namespace ASPNETapp2.Models
{
    public class OrderItem
    {
        public Meal Meal { get; set; }
        public int Quantity { get; set; }
        public double ListPositionPrice { get; set; }

        public OrderItem(Meal meal, int quantity, double listPositionPrice)
        {
            Meal = meal;
            Quantity = quantity;
            ListPositionPrice = listPositionPrice;
        }
        public OrderItem(Meal meal, double listPositionPrice)
        {
            Meal = meal;
            ListPositionPrice = listPositionPrice;
        }
    }
}
