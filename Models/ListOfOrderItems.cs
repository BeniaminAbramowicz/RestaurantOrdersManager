using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETapp2.Models
{
    public class ListOfOrderItems
    {
        private List<OrderItem> _itemsList;
        public List<OrderItem> ItemsList { get => _itemsList; set => _itemsList = value; }
    }
}