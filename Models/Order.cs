using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPNETapp2.Models;

namespace ASPNETapp2.Models
{
    public class Order
    {
        private int _orderId;
        public int OrderId { get => _orderId; set => _orderId = value; }

        private List<OrderItem> _orderList;
        public List<OrderItem> OrderList { get => _orderList; set => _orderList = value; }

        private double _totalPrice;
        public double TotalPrice { get => _totalPrice; set => _totalPrice = value; }

    }
}