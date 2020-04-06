using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETapp2.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public double MealUnitPrice { get; set; }

        public Meal(int mealId, string mealName, double mealUnitPrice)
        {
            MealId = mealId;
            MealName = mealName;
            MealUnitPrice = mealUnitPrice;
        }
        public Meal(string mealName)
        {
            MealName = mealName;
        }
    }

    public static class MealsList
    {
        public static List<Meal> theList = new List<Meal> 
        { new Meal(1, "Roladki z cukinii z szynką", 12),
          new Meal(2, "Kotlet schabowy z ziemniakami i mizerią", 25),
          new Meal(3, "Szarlotka z jabłkami", 14.5),
          new Meal(4, "Gofry pieczone w piekarniku", 15),
          new Meal(5, "Tabbouleh sałatka arabska z kaszą bulgur", 30),
          new Meal(6, "Kaszanka pieczona z kiszoną kapustą", 10),
          new Meal(7, "Sałatka z zupek chińskich", 12),
          new Meal(8, "Pasta z dyni do chleba", 13.5)
    };

    }
}