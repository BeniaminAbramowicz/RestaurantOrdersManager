using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETapp2.Models
{
    public class MealItem
    {
        private int _mealItemId;
        public int MealItemId { get => _mealItemId; set => _mealItemId = value; }

        private int _mealQuantity;
        public int MealQuantity { get => _mealQuantity; set => _mealQuantity = value; }
    }
}