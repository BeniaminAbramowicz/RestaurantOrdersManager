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
        public Table Table { get; set; }
        public OrderStatus Status { get; set; }
        
        public Order() { }
        public Order(List<OrderItem> orderItems, double totalPrice, Table table, OrderStatus status)
        {
            OrderItems = orderItems;
            TotalPrice = totalPrice;
            Table = table;
            Status = status;
        }
    }
}