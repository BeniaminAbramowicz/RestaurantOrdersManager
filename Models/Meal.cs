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

    public static class MealsList
    {
        public static Meal Meal1 = new Meal(1, "Roladki z cukinii z szynką", 12);
        public static Meal Meal2 = new Meal(2, "Kotlet schabowy z ziemniakami i mizerią", 25);
        public static Meal Meal3 = new Meal(3, "Szarlotka z jabłkami", 14.5);
    }
}