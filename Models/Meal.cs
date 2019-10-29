using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETapp2.Models
{
    public class Meal
    {
        private int _mealId;
        public int MealId { get => _mealId; set => _mealId = value; }
        
        private string _mealName;
        public string MealName { get => _mealName; set => _mealName = value; }
        
        private double _mealUnitPrice;
        public double MealUnitPrice { get => _mealUnitPrice; set => _mealUnitPrice = value; }

        public Meal(int mealId, string mealName, double mealUnitPrice)
        {
            MealId = mealId;
            MealName = mealName;
            MealUnitPrice = mealUnitPrice;
        }
    }
}