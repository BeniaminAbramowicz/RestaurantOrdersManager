using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPNETapp2.Models;

namespace ASPNETapp2.Models
{
    public class OrderItem
    {
        private Meal _meal;
        public Meal Meal { get => _meal; set => _meal = value; }
        
        private int _quantity;
        public int Quantity { get => _quantity; set => _quantity = value; }

        private double _listPositionPrice;
        public double ListPositionPrice { get => _listPositionPrice; set => _listPositionPrice = value; }
    }
}
