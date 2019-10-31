using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETapp2.Models
{
    public class AvailableMeals
    {
        private List<Meal> _listOfMeals;
        public List<Meal> ListOfMeals { get => _listOfMeals; set => _listOfMeals = value; }
    }
}