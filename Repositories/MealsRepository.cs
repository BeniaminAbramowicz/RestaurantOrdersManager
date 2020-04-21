using ASPNETapp2.Models;
using System.Collections.Generic;
using System.Linq;

namespace ASPNETapp2.Repositories
{
    public class MealsRepository : IExtendedRepository<ResponseObject<Meal>, Meal>
    {
        public ResponseObject<Meal> FindAll(SearchCondition condition)
        {
            try
            {
                IEnumerable<Meal> mealsList = DBConnection.EntityMapper.QueryForList<Meal>("GetMealsList", condition);
                if(mealsList == null || !mealsList.Any())
                {
                    return new ResponseObject<Meal>(){ Message = "List of meals is empty" };
                }
                else
                {
                    return new ResponseObject<Meal>() { ResponseList = mealsList };
                }
            }
            catch
            {
                return new ResponseObject<Meal> { Message = "There was an error processing the request. Try again later" };
            } 
        }

        public ResponseObject<Meal> FindById(int mealId)
        {
            try
            {
                ResponseObject<Meal> mealResponse = new ResponseObject<Meal>() 
                { 
                    ResponseData = DBConnection.EntityMapper.QueryForObject<Meal>("GetMealById", mealId) 
                };
                if(mealResponse.ResponseData == null)
                {
                    mealResponse.Message = "Meal with a given id doesn't exist in database";
                    return mealResponse;
                }
                else
                {
                    return mealResponse;
                }
            }
            catch
            {
                return new ResponseObject<Meal>(){ Message = "There was an error processing the request. Try again later" };
            }
        }

        public ResponseObject<Meal> FindByName(string mealName)
        {
            try
            {
                ResponseObject<Meal> mealResponse = new ResponseObject<Meal>()
                {
                    ResponseData = DBConnection.EntityMapper.QueryForObject<Meal>("GetMealByName", mealName)
                };
                if (mealResponse.ResponseData == null)
                {
                    mealResponse.Message = "Meal with a given name doesn't exist in database";
                    return mealResponse;
                }
                else
                {
                    return mealResponse;
                }
            }
            catch
            {
                return new ResponseObject<Meal>(){ Message = "There was an error processing the request. Try again later" };
            }
        }

        public ResponseObject<Meal> Add(Meal newMeal)
        {
            try
            {
                DBConnection.EntityMapper.Insert("AddMeal", newMeal);
                ResponseObject<Meal> mealResponse = new ResponseObject<Meal>()
                {
                    ResponseData = FindById(DBConnection.EntityMapper.QueryForObject<int>("ReturnMeal", "")).ResponseData
                };
                return mealResponse;
            }
            catch
            {
                return new ResponseObject<Meal>(){ Message = "There was an error while adding new meal. Try again later" };
            }
        }

        public ResponseObject<Meal> Remove(int mealId)
        {
            try
            {
                DBConnection.EntityMapper.Delete("RemoveMeal", mealId);
                ResponseObject<Meal> response = new ResponseObject<Meal>()
                {
                    Message = "Successfully removed the meal"
                };
                return response;
            }
            catch
            {
                return new ResponseObject<Meal>(){ Message = "There was an error while removing the meal. Try again later" };
            }
        }

        public ResponseObject<Meal> Update(Meal updatedMeal)
        {
            try
            {
                DBConnection.EntityMapper.Update("UpdateMeal", updatedMeal);
                ResponseObject<Meal> mealResponse = new ResponseObject<Meal>()
                {
                    ResponseData = FindById(updatedMeal.MealId).ResponseData
                };
                mealResponse.Message = "Successfully updated the meal";
                return mealResponse;
            }
            catch
            {
                return new ResponseObject<Meal>() { Message = "There was an error while updating the meal. Try again later" };
            }
        }
    }
}