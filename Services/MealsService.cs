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
        public Meal AddMeal(MealDTO newMeal)
        {
            return _mealsRepository.AddMeal(newMeal);
        }
        public void RemoveMeal(int mealId)
        {
            _mealsRepository.RemoveMeal(mealId);
        }
        public Meal UpdateMeal(Meal meal)
        {
            return _mealsRepository.UpdateMeal(meal);
        }
    }
}