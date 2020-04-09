using ASPNETapp2.Models;
using System.Collections.Generic;

namespace ASPNETapp2.Services
{
    public interface IMealsService
    {
        IEnumerable<Meal> FindAll();
        Meal FindById(int mealId);
        Meal FindByName(string mealName);
        Meal AddMeal(MealDTO newMeal);
        void RemoveMeal(int mealId);
        Meal UpdateMeal(Meal meal);
    }
}