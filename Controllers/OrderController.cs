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

        public JsonResult CreateOrder(SentOrder sentOrder)
        {
            if (Session["ListOfOrders"] == null)
            {
                Session["ListOfOrders"] = new List<Order>();
            }
            double pr = 0;
            foreach (var x in sentOrder.SentOrderItems)
            {
                pr += MealsList.theList.Find(z => z.MealName.Equals(x.SentMealName)).MealUnitPrice * x.SentQuantity;
            }
            List<OrderItem> finalItems = new List<OrderItem>();
            foreach (var y in sentOrder.SentOrderItems)
            {
                finalItems.Add(new OrderItem(MealsList.theList.Find(x => x.MealName.Equals(y.SentMealName)), y.SentQuantity, (MealsList.theList.Find(z => z.MealName.Equals(y.SentMealName)).MealUnitPrice)*y.SentQuantity));
            }
            Order newRecievedOrder = new Order(finalItems, pr, sentOrder.SentTableNumber, Order.OrderStatus.PendingPayment);
            var getSessionList = (List<Order>)Session["ListOfOrders"];
            getSessionList.Add(newRecievedOrder);
            Session["ListOfOrders"] = getSessionList;
            string retMsg = "success";
            return Json(retMsg, JsonRequestBehavior.AllowGet);
        }
    }
}