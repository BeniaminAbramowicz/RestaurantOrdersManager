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

        [HttpPost]
        public JsonResult AddItem(MealItem mealItem)
        {
            Meal theMeal = MealsList.theList.Find(x => x.MealId == mealItem.MealItemId);
            var resultData = new { Name = theMeal.MealName, 
                                   Price = theMeal.MealUnitPrice, 
                                   Quantity = mealItem.MealQuantity, 
                                   SummedPrice = theMeal.MealUnitPrice * mealItem.MealQuantity};
            return Json(resultData);
        }

        [HttpPost]
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
            return Json("");
        }

       [HttpPost]
       public JsonResult RemoveOrder(int idOfOrder)
        {
            List<Order> currentList = (List<Order>)Session["ListOfOrders"];
            currentList.Remove(currentList.Find(x => x.OrderId == idOfOrder));
            Session["ListOfOrders"] = currentList;
            return Json("");
        }

        [HttpPost]
        public ActionResult GetSummary(int orderId)
        {
            TempData["orderId"] = orderId;
            return Redirect("OrderSummary");
        }
        public ActionResult OrderSummary()
        {
            int orderId = (int)TempData["orderId"];
            List<Order> currentList = (List<Order>)Session["ListOfOrders"];
            Order summaryOrder = currentList.Find(x => x.OrderId == orderId);
            if (summaryOrder.OrderItems.Exists(z => z.Meal.MealName.Equals("Napiwek 5%")))
            {
                TempData["orderId"] = orderId;
                return View(summaryOrder);
            }
            else
            {
                OrderItem tipItem = new OrderItem(new Meal("Napiwek 5%"), summaryOrder.TotalPrice * 0.05);
                summaryOrder.OrderItems.Add(tipItem);
                summaryOrder.Status = Order.OrderStatus.BillPaid;
                currentList[currentList.FindIndex(y => y.OrderId == orderId)] = summaryOrder;
                Session["ListOfOrders"] = currentList;
                TempData["orderId"] = orderId;
                return View(summaryOrder);
            }
            
        }
    }
}