﻿using System.Linq;
using System.Web.Mvc;
using ASPNETapp2.Models;
using ASPNETapp2.Facades;

namespace ASPNETapp2.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestaurantFacade _restaurantFacade;

        public HomeController()
        {
            _restaurantFacade = new RestaurantFacade();
        }

        [HttpGet]
        public ActionResult Index()
        {
            SearchCondition empty = new SearchCondition("");
            ListOfOrdersMealsTables indexList = new ListOfOrdersMealsTables()
            {
                TablesList = _restaurantFacade.FindAllTables(empty).ResponseList.ToList(),
                MealsList = _restaurantFacade.FindAllMeals(empty).ResponseList.ToList()
            };
            string chosenTable = "";
            if (TempData["ChosenTable"] != null)
            {
                chosenTable = TempData["ChosenTable"].ToString();
            }
            SearchCondition tableCondition = new SearchCondition(chosenTable);
            switch (chosenTable)
            {
                case "all":
                case "empty":
                    indexList.OrdersList = _restaurantFacade.FindAllOrders(empty).ResponseList.ToList();
                    break;
                case "billpaid":
                    indexList.OrdersList = _restaurantFacade.FindAllOrders(tableCondition).ResponseList.ToList();
                    break;
                case "pendingpayment":
                    indexList.OrdersList = _restaurantFacade.FindAllOrders(tableCondition).ResponseList.ToList();
                    break;
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                    indexList.OrdersList = _restaurantFacade.FindAllOrders(tableCondition).ResponseList.ToList();
                    break;  
            }
            return View(indexList);
        }

        [HttpPost]
        public ActionResult DisplayOrders(string chosenTable)
        {
            TempData["ChosenTable"] = chosenTable;
            return Redirect("Index");
        }
    }
}