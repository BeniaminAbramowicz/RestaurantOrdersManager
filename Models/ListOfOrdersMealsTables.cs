using System.Collections.Generic;

namespace ASPNETapp2.Models
{
    public class ListOfOrdersMealsTables
    {
        public List<Order> OrdersList { get; set; }
        public List<Table> TablesList { get; set; }
        public List<Meal> MealsList { get; set; }
    }
}