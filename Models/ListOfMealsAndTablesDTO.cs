using System.Collections.Generic;

namespace ASPNETapp2.Models
{
    public class ListOfMealsAndTablesDTO
    {
        public List<Meal> MealsList { get; set; }
        public List<Table> TablesList { get; set; }
    }
}