using ASPNETapp2.Models;
using ASPNETapp2.Repositories;
using System.Collections.Generic;

namespace ASPNETapp2.Services
{
    public class MealsService : IMealsService
    {
        private readonly IMealsRepository _mealsRepository;

        public MealsService()
        {
            _mealsRepository = new MealsRepository();
        }

        public IEnumerable<Meal> FindAll()
        {
            return _mealsRepository.FindAll();
        }
        public Meal FindById(int mealId)
        {
            return _mealsRepository.FindById(mealId);
        }
        public Meal FindByName(string mealName)
        {
            return _mealsRepository.FindByName(mealName);
        }
        public Meal Add(Meal newMeal)
        {
            return _mealsRepository.Add(newMeal);
        }
        public void Remove(int mealId)
        {
            _mealsRepository.Remove(mealId);
        }
        public Meal Update(Meal meal)
        {
            return _mealsRepository.Update(meal);
        }
    }
}