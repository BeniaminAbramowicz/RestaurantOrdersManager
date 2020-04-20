using System.Collections.Generic;
using System.Linq;
using ASPNETapp2.Models;
using System.Web.Mvc;
using ASPNETapp2.Services;
using System;

namespace ASPNETapp2.Controllers
{
    public class MealAndTableController : Controller
    {
        private readonly RestaurantFacade _restaurantFacade;

        public MealAndTableController()
        {
            _restaurantFacade = new RestaurantFacade();
        }

        public ActionResult Index()
        {
            int? choice = Int32.Parse(TempData["choice"] == null ? "0" : TempData["choice"].ToString());
            switch (choice)
            {
                case 1:
                    ListOfMealsTables mealsList = new ListOfMealsTables()
                    {
                        MealsList = _restaurantFacade.FindAllMeals(new SearchCondition("")).ToList()
                    };
                    return View(mealsList);
                case 2:
                    ListOfMealsTables tablesList = new ListOfMealsTables()
                    {
                        TablesList = _restaurantFacade.FindAllTables(new SearchCondition("")).ToList()
                    };
                    return View(tablesList);
                default:
                    ListOfMealsTables emptyList = new ListOfMealsTables()
                    {
                        MealsList = new List<Meal>(),
                        TablesList = new List<Table>()
                    };
                    return View(emptyList);
            }
        }

        [HttpPost]
        public ActionResult ChooseMealsOrTables(int choice)
        {
            TempData["choice"] = choice;
            return RedirectToAction("Index", "MealAndTable");
        }

        [HttpPost]
        public JsonResult RemoveMeal(int mealId)
        {
            _restaurantFacade.RemoveMeal(mealId);
            return Json("");
        }

        [HttpPost]
        public JsonResult RemoveTable(int tableId)
        {
            _restaurantFacade.RemoveTable(tableId);
            return Json("");
        }

        [HttpPost]
        public JsonResult AddMeal(MealDTO newMeal)
        {
            Meal addedMeal = _restaurantFacade.AddMeal(new Meal(newMeal));
            return Json(addedMeal);
        }

        [HttpPost]
        public JsonResult AddTable(string tableName)
        {
            Table addedTable = _restaurantFacade.AddTable(new Table(tableName));
            return Json(addedTable);
        }

        [HttpPut]
        public JsonResult UpdateMeal(Meal meal)
        {
            Meal updatedMeal = _restaurantFacade.UpdateMeal(meal);
            return Json(updatedMeal);
        }

        [HttpPut]
        public JsonResult UpdateTable(Table table)
        {
            Table updatedTable = _restaurantFacade.UpdateTable(table);
            return Json(updatedTable);
        }
    }
}