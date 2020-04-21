using ASPNETapp2.Models;
using ASPNETapp2.Repositories;

namespace ASPNETapp2.Services
{
    public class MealsService : IExtendedService<ResponseObject<Meal>,Meal>
    {
        private readonly IExtendedRepository<ResponseObject<Meal>,Meal> _mealsRepository;

        public MealsService()
        {
            _mealsRepository = new MealsRepository();
        }

        public ResponseObject<Meal> FindAll(SearchCondition condition)
        {
            return _mealsRepository.FindAll(condition);
        }
        public ResponseObject<Meal> FindById(int mealId)
        {
            return _mealsRepository.FindById(mealId);
        }
        public ResponseObject<Meal> FindByName(string mealName)
        {
            return _mealsRepository.FindByName(mealName);
        }
        public ResponseObject<Meal> Add(Meal newMeal)
        {
            ResponseObject<Meal> mealExists = new ResponseObject<Meal>()
            {
                ResponseData = FindByName(newMeal.MealName).ResponseData
            };
            if(mealExists.ResponseData == null)
            {
                return _mealsRepository.Add(newMeal);
            } 
            else
            {
                return new ResponseObject<Meal>() { Message = "Meal already exists in the database" };
            }
        }
        public ResponseObject<Meal> Remove(int mealId)
        {
            ResponseObject<Meal> mealExists = new ResponseObject<Meal>()
            {
                ResponseData = FindById(mealId).ResponseData
            }; 
            if(mealExists.ResponseData != null)
            {
                _mealsRepository.Remove(mealId);
                return new ResponseObject<Meal>() { Message = "Successfully removed the meal" };
            }
            else
            {
                return new ResponseObject<Meal>() { Message = "Chosen meal doesn't exist in the database" };
            } 
        }
        public ResponseObject<Meal> Update(Meal updatedMeal)
        {
            ResponseObject<Meal> mealExists = new ResponseObject<Meal>()
            {
                ResponseData = FindById(updatedMeal.MealId).ResponseData
            }; 
            if(mealExists != null)
            {
                return _mealsRepository.Update(updatedMeal);
            }
            else
            {
                return new ResponseObject<Meal>() { Message = "Chosen meal doesn't exist in the database" };
            }
        }
    }
}