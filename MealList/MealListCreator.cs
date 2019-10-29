using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPNETapp2.Models;

namespace ASPNETapp2.MealList
{
    public class MealListCreator
    {
        private static List<Meal> _mealList;
        public static List<Meal> MealList { get => _mealList; set => _mealList = value; }
        public static List<Meal> CreateMealList()
        {
            Meal appetizer = new Meal(1, "Roladki z cukinii z szynką", 12);
            Meal dinner = new Meal(2, "Kotlet schabowy z ziemniakami i mizerią", 25);
            Meal dessert = new Meal(3, "Szarlotka z jabłkami", 14.5);

            List<Meal> tempList = new List<Meal> { appetizer, dinner, dessert };
            MealList = tempList;

            return MealList;
        }
    }
}