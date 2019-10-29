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
            return View(order);
        }

        public ActionResult AddOrder()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}