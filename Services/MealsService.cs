using ASPNETapp2.Models;
using ASPNETapp2.Repositories;
using System.Collections.Generic;

namespace ASPNETapp2.Services
{
    public class MealsService : IExtendedService<Meal>
    {
        private readonly IExtendedRepository<Meal> _mealsRepository;

        public MealsService()
        {
            _mealsRepository = new MealsRepository();
        }

        public IEnumerable<Meal> FindAll(SearchCondition condition)
        {
            return _mealsRepository.FindAll(condition);
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
            Meal mealExists = FindByName(newMeal.MealName);
            if(mealExists == null)
            {
                return _mealsRepository.Add(newMeal);
            } else
            {
                return null;
            }
        }
        public string Remove(int mealId)
        {
            Meal mealExists = FindById(mealId);
            if(mealExists != null)
            {
                _mealsRepository.Remove(mealId);
                return "Pomyślnie usunięto posiłek";
            }
            else
            {
                return "Wybrany posiłek nie istnieje w bazie posiłków";
            } 
        }
        public Meal Update(Meal updatedMeal)
        {
            Meal mealExists = FindById(updatedMeal.MealId);
            if(mealExists != null)
            {
                return _mealsRepository.Update(updatedMeal);
            }
            else
            {
                return null;
            }
        }
    }
}