using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPNETapp2.Models;

namespace ASPNETapp2.Models
{
    public class Order
    {
        public enum OrderStatus
        {
            PendingPayment,
            BillPaid
        }

        private int _orderId;
        public int OrderId { get => _orderId; set => _orderId = value; }

        private List<OrderItem> _orderItems;
        public List<OrderItem> OrderItems { get => _orderItems; set => _orderItems = value; }

        private double _totalPrice;
        public double TotalPrice { get => _totalPrice; set => _totalPrice = value; }

        private int _tableNumber;
        public int TableNumber { get => _tableNumber; set => _tableNumber = value; }

        private OrderStatus _Status;
        public OrderStatus Status { get => _Status; set => _Status = value; }

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