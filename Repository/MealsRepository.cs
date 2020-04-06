using ASPNETapp2.Models;
using System.Collections.Generic;

namespace ASPNETapp2.Repository
{
    public class MealsRepository
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

        public void AddMeal(Meal newMeal)
        {
            DBConnection.EntityMapper.Insert("AddMeal", newMeal);
        }

        public void RemoveMeal(int mealId)
        {
            DBConnection.EntityMapper.Delete("RemoveMeal", mealId);
        }

        public void UpdateMeal(Meal meal)
        {
            DBConnection.EntityMapper.Update("UpdateMeal", meal);
        }
    }
}