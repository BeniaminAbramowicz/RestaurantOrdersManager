using ASPNETapp2.Models;
using System.Collections.Generic;

namespace ASPNETapp2.Repositories
{
    public class MealsRepository : IExtendedRepository<Meal>
    {
        public IEnumerable<Meal> FindAll(SearchCondition condition)
        {
            return DBConnection.EntityMapper.QueryForList<Meal>("GetMealsList", condition);
        }

        public Meal FindById(int mealId)
        {
            return DBConnection.EntityMapper.QueryForObject<Meal>("GetMealById", mealId);
        }

        public Meal FindByName(string mealName)
        {
            return DBConnection.EntityMapper.QueryForObject<Meal>("GetMealByName", mealName);
        }

        public Meal Add(Meal newMeal)
        {
            DBConnection.EntityMapper.Insert("AddMeal", newMeal);           
            return FindById(DBConnection.EntityMapper.QueryForObject<int>("ReturnMeal", ""));
        }

        public void Remove(int mealId)
        {
            DBConnection.EntityMapper.Delete("RemoveMeal", mealId);
        }

        public Meal Update(Meal updatedMeal)
        {
            DBConnection.EntityMapper.Update("UpdateMeal", updatedMeal);
            return FindById(updatedMeal.MealId);
        }
    }
}