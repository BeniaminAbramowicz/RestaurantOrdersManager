using System.Collections.Generic;
using ASPNETapp2.Models;

namespace ASPNETapp2.Services
{
    public class RestaurantFacade
    {
        private readonly IService<Meal> _mealsService;
        private readonly IService<Table> _tablesService;

        public RestaurantFacade()
        {
            _mealsService = new MealsService();
            _tablesService = new TablesService();
        }

        public IEnumerable<Meal> FindAllMeals()
        {
            return _mealsService.FindAll();
        }

        public Meal FindMealById(int mealId)
        {
            return _mealsService.FindById(mealId);
        }

        public Meal FindMealByName(string mealName)
        {
            return _mealsService.FindByName(mealName);
        }

        public Meal AddMeal(Meal newMeal)
        {
            return _mealsService.Add(newMeal);
        }

        public string RemoveMeal(int mealId)
        {
            return _mealsService.Remove(mealId);
        }

        public Meal UpdateMeal(Meal updatedMeal)
        {
            return _mealsService.Update(updatedMeal);
        }

        public IEnumerable<Table> FindAllTables()
        {
            return _tablesService.FindAll();
        }

        public Table FindTableById(int tableId)
        {
            return _tablesService.FindById(tableId);
        }

        public Table AddTable(Table newTable)
        {
            return _tablesService.Add(newTable);
        }

        public string RemoveTable(int tableId)
        {
            return _tablesService.Remove(tableId);
        }

        public Table UpdateTable(Table updatedTable)
        {
            return _tablesService.Update(updatedTable);
        }
    }
}