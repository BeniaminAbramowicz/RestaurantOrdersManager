using ASPNETapp2.Models;
using System.Collections.Generic;

namespace ASPNETapp2.Repositories
{
    public class MealsRepository : IMealsRepository
    {
        public IEnumerable<Meal> FindAll()
        {
            return DBConnection.EntityMapper.QueryForList<Meal>("GetMealsList", "");
        }

        public Meal FindById(int mealId)
        {
            return DBConnection.EntityMapper.QueryForObject<Meal>("GetMealById", mealId);
        }

        public Meal FindByName(string mealName)
        {
            return DBConnection.EntityMapper.QueryForObject<Meal>("GetMealByName", mealName);
        }

        public Meal AddMeal(MealDTO newMeal)
        {
            DBConnection.EntityMapper.Insert("AddMeal", newMeal);
            int newMealId = DBConnection.EntityMapper.QueryForObject<int>("ReturnMeal", "");
            
            return FindById(newMealId);
        }

        public void RemoveMeal(int mealId)
        {
            DBConnection.EntityMapper.Delete("RemoveMeal", mealId);
        }

        public Meal UpdateMeal(Meal meal)
        {
            DBConnection.EntityMapper.Update("UpdateMeal", meal);
            return FindById(meal.MealId);
        }
    }
}