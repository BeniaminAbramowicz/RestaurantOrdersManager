using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNETapp2.Models;

namespace ASPNETapp2.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult AddOrder()
        {
            AvailableMeals availableMeals = new AvailableMeals
            {
                ListOfMeals = MealsList.theList
            };
            return View(availableMeals);
        }

        public JsonResult AddItem(MealItem mealItem)
        {
            Meal theMeal = MealsList.theList.Find(x => x.MealId == mealItem.MealItemId);
            var resultData = new { Name = theMeal.MealName, 
                                   Price = theMeal.MealUnitPrice, 
                                   Quantity = mealItem.MealQuantity, 
                                   SummedPrice = theMeal.MealUnitPrice * mealItem.MealQuantity};
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrder(Order sentOrder)
        {
            double pr = 0;
            foreach(var x in sentOrder.OrderItems)
            {
                pr += x.ListPositionPrice;
            }

            Order nowy = new Order(sentOrder.OrderItems, pr, sentOrder.TableNumber, Order.OrderStatus.PendingPayment);
            List<Order> listOfOrders = (List<Order>) Session["listOfOrders"];
            listOfOrders.Add(nowy);
            Session["listOfOrders"] = listOfOrders;

            return Redirect("AddOrder");
        }
    }
}