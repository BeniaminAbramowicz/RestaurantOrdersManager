using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPNETapp2.Models;
using System.Web.Mvc;
using ASPNETapp2.Services;

namespace ASPNETapp2.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealsService _mealsService;

        public MealController()
        {
            _mealsService = new MealsService();
        }

        public ActionResult MealsList()
        {
            ListOfMealsDTO mealsList = new ListOfMealsDTO()
            {
                MealsList = (List<Meal>)_mealsService.FindAll()
            };
            return View(mealsList);
        }

        [HttpPost]
        public JsonResult RemoveMeal(int mealId)
        {
            _mealsService.RemoveMeal(mealId);
            return Json("");
        }

        [HttpPost]
        public JsonResult AddMeal(MealDTO newMeal)
        {
            Meal addedMeal = _mealsService.AddMeal(newMeal);
            return Json(addedMeal);
        }

        [HttpPut]
        public JsonResult UpdateMeal(Meal meal)
        {
            Meal updatedMeal =_mealsService.UpdateMeal(meal);
            return Json(updatedMeal);
        }
    }
}