using System.Threading;
using System.Collections.Generic;

namespace ASPNETapp2.Models
{
    public class Order
    {
        public enum OrderStatus
        {
            PendingPayment,
            BillPaid
        }
        public int OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public double TotalPrice { get; set; }
        public int TableNumber { get; set; }
        public OrderStatus Status { get; set; }
        public static int GlobalOrderId = 0;
        
        public Order(List<OrderItem> orderItems, double totalPrice, int tableNumber, OrderStatus status)
        {
            OrderId = Interlocked.Increment(ref GlobalOrderId);
            OrderItems = orderItems;
            TotalPrice = totalPrice;
            TableNumber = tableNumber;
            Status = status;

        }
    }
}