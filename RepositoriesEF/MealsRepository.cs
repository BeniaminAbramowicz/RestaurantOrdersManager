using ASPNETapp2.Models;
using System.Linq;

namespace ASPNETapp2.RepositoriesEF
{
    public class MealsRepository : IExtendedRepository<ResponseObject<Meal>, Meal>
    {
        private readonly RestaurantDBContext _dbContext;

        public MealsRepository()
        {
            _dbContext = new RestaurantDBContext();
        }

        public ResponseObject<Meal> Add(Meal newMeal)
        {
            _dbContext.Meals.Add(newMeal);
            _dbContext.SaveChanges();
            var meal = _dbContext.Meals.FirstOrDefault(i => i.MealId == newMeal.MealId);
            return new ResponseObject<Meal>() { ResponseData = meal , Message = "Meal has been added to list" };
        }

        public ResponseObject<Meal> FindAll(SearchCondition condition)
        {
            var mealsList = from x in _dbContext.Meals select x;
            return new ResponseObject<Meal>() { ResponseList = mealsList };
        }

        public ResponseObject<Meal> FindById(int id)
        {
            var meal = from x in _dbContext.Meals where x.MealId == id select x;
            return new ResponseObject<Meal>() { ResponseData = meal.First() };
        }

        public ResponseObject<Meal> FindByName(string name)
        {
            var meal = from x in _dbContext.Meals where x.MealName == name select x;
            return new ResponseObject<Meal>() { ResponseData = meal.First() };
        }

        public ResponseObject<Meal> Remove(int id)
        {
            var meal = from x in _dbContext.Meals where x.MealId == id select x;
            _dbContext.Meals.Remove(meal.First());
            _dbContext.SaveChanges();
            return new ResponseObject<Meal>() { Message = "Chosen meal has been removed" };
        }

        public ResponseObject<Meal> Update(Meal updatedMeal)
        {
            var meal = (from x in _dbContext.Meals where x.MealId == updatedMeal.MealId select x).First();
            meal.MealName = updatedMeal.MealName;
            meal.MealUnitPrice = updatedMeal.MealUnitPrice;
            _dbContext.SaveChanges();
            return new ResponseObject<Meal>() { ResponseData = meal, Message = "Chosen meal has been updated" };
        }
    }
}