using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNETapp2.Models;
using ASPNETapp2.MealList;

namespace ASPNETapp2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Order pOrder = (Order) Session["po"];
            if(pOrder == null)
            {
                Order emptyOrder = new Order();
                return View(emptyOrder);
            }
            else
            {
                return View(pOrder);
            }         
        }

        [HttpPost]
        [ActionName("DisplayOrders")]
        public ActionResult DisplayOrders(string chosenTable)
        {
            Order order = new Order();
            Meal meal = new Meal(1, "Roladki z cukinii z szynką", 12);
            Meal meal2 = new Meal(1, "test", 12);
            OrderItem orderItem = new OrderItem();
            OrderItem orderItem2 = new OrderItem();
            orderItem.Meal = meal;
            orderItem.Quantity = 2;
            orderItem.ListPositionPrice = orderItem.Meal.MealUnitPrice * orderItem.Quantity;
            orderItem2.Meal = meal2;
            orderItem2.Quantity = 2;
            orderItem2.ListPositionPrice = orderItem2.Meal.MealUnitPrice * orderItem2.Quantity;
            List<OrderItem> orderItems = new List<OrderItem> { orderItem, orderItem2 };
            order.OrderId = 1;
            order.OrderList = orderItems;
            double finalPrice = 0;
            foreach (var item in order.OrderList)
            {
                finalPrice += item.ListPositionPrice;
            }          
            order.TotalPrice = finalPrice;
            order.TableNumber = 1;
            int x = Int32.Parse(chosenTable);
            List<Order> listOfOrders = new List<Order> { order };

            var passingOrder = listOfOrders.Find(item => item.TableNumber == x);
            Session["po"] = passingOrder;

            return Redirect("Index");
        }

        public ActionResult AddOrder()
        {
            return View();
        }

    }
}