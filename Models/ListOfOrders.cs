using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETapp2.Models
{
    public class ListOfOrders
    {
        private List<Order> _ordersList;
        public List<Order> OrdersList { get => _ordersList; set => _ordersList = value; }
    }
}