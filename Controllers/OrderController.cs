﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ASPNETapp2.Models;
using ASPNETapp2.Services;

namespace ASPNETapp2.Controllers
{
    public class OrderController : Controller
    {
        private readonly RestaurantFacade _restaurantFacade;

        public OrderController()
        {
            _restaurantFacade = new RestaurantFacade();
        }

        public ActionResult AddOrder()
        {
            ListOfMealsTables listOfMealsAndTables = new ListOfMealsTables()
            {
                MealsList = _restaurantFacade.FindAllMeals(new SearchCondition("")).ResponseList.ToList(),
                TablesList = _restaurantFacade.FindAllTables(new SearchCondition("")).ResponseList.ToList()
            };
            return View(listOfMealsAndTables);
        }

        [HttpPost]
        public JsonResult CreateOrder(OrderDTO receivedOrder)
        {
            double totalPrice = 0;
            ResponseObject<Table> chosenTable = _restaurantFacade.FindTableById(receivedOrder.TableNumber);
            List<Meal> mealsList = new List<Meal>();
            List<OrderItem> orderItems = new List<OrderItem>();
            ResponseObject<Meal> currentMeal;
            foreach (var sentOrderItem in receivedOrder.OrderItems)
            {
                currentMeal = _restaurantFacade.FindMealByName(sentOrderItem.MealName);
                mealsList.Add(currentMeal.ResponseData);
                totalPrice += currentMeal.ResponseData.MealUnitPrice * sentOrderItem.Quantity;
            }
            for(var i = 0; i < receivedOrder.OrderItems.Count; i++)
            {
                orderItems.Add(new OrderItem(mealsList[i], receivedOrder.OrderItems[i].Quantity, mealsList[i].MealUnitPrice * receivedOrder.OrderItems[i].Quantity));
            }
            _restaurantFacade.AddOrder(new Order(orderItems, totalPrice, chosenTable.ResponseData, Order.OrderStatus.PendingPayment));
            return Json("");
        }

        [HttpPost]
        public JsonResult RemovePosition(int orderId, int orderItemId)
        {
            double price = _restaurantFacade.RemovePosition(orderItemId, orderId).ResponseData.TotalPrice;
            return Json(new { Data = price });
        }

        [HttpPost]
        public JsonResult RemoveOrder(int orderId)
        {
            _restaurantFacade.RemoveOrder(orderId);
            return Json("");
        }

        // [HttpPost]
        // public ActionResult GetSummary(int orderId)
        // {
        //     TempData["orderId"] = orderId;
        //     return Redirect("OrderSummary");
        // }
        //public ActionResult OrderSummary()
        //{
        //    int orderId = (int)TempData["orderId"];
        //    List<Order> currentList = (List<Order>)Session["ListOfOrders"];
        //    Order summaryOrder = currentList.Find(x => x.OrderId == orderId);
        //    if (summaryOrder.OrderItems.Exists(z => z.Meal.MealName.Equals("Napiwek 5%")))
        //    {
        //        TempData["orderId"] = orderId;
        //        return View(summaryOrder);
        //    }
        //    else
        //    {
        //        OrderItem tipItem = new OrderItem(new Meal("Napiwek 5%"), Math.Round(summaryOrder.TotalPrice * 0.05, 2));
        //        summaryOrder.TotalPrice = Math.Round(summaryOrder.TotalPrice * 1.05, 2);  
        //        summaryOrder.OrderItems.Add(tipItem);
        //        summaryOrder.Status = Order.OrderStatus.BillPaid;
        //        currentList[currentList.FindIndex(y => y.OrderId == orderId)] = summaryOrder;
        //        Session["ListOfOrders"] = currentList;
        //        TempData["orderId"] = orderId;
        //        return View(summaryOrder);
        //    }

        //}

        [HttpPost]
        public JsonResult AddPosition(int mealId, int quantity, int orderId)
        {
            Meal mealToAdd = _restaurantFacade.FindMealById(mealId).ResponseData;
            OrderItem newPosition = new OrderItem()
            {
                Meal = mealToAdd,
                Price = mealToAdd.MealUnitPrice * quantity,
                Quantity = quantity,
                OrderId = orderId
            };
            OrderItem newItem = _restaurantFacade.AddPosition(newPosition).ResponseData;
            double totalPrice = _restaurantFacade.FindOrderById(orderId).ResponseData.TotalPrice;
            return Json(new { Data = newItem, TotalPrice = totalPrice });
        }

        //public ActionResult EditView(int orderId)
        //{
        //    TempData["idOfOrder"] = orderId;
        //    List<Order> currentList = (List<Order>)Session["ListOfOrders"];
        //    Order order = currentList.Find(x => x.OrderId == orderId);
        //    ListOfOrderItemsDTO listOfOrderItems = new ListOfOrderItemsDTO { ItemsList = order.OrderItems };

        //    return View(listOfOrderItems);
        //}

        //[HttpPost]
        //public ActionResult EditPosition(string replacedMeal, string chosenMeal, int quantity)
        //{
        //    int idOfOrder = (int)TempData["idOfOrder"];
        //    List<Order> currentList = (List<Order>)Session["ListOfOrders"];
        //    Order order = currentList.Find(x => x.OrderId == idOfOrder);
        //    if (replacedMeal == chosenMeal)
        //    {
        //        if(order.OrderItems[order.OrderItems.FindIndex(s => s.Meal.MealName.Equals(chosenMeal))].Quantity > quantity)
        //        {
        //            order.OrderItems[order.OrderItems.FindIndex(g => g.Meal.MealName.Equals(chosenMeal))].Price -= MealsList.theList.Find(u => u.MealName.Equals(chosenMeal)).MealUnitPrice * (order.OrderItems[order.OrderItems.FindIndex(s => s.Meal.MealName.Equals(chosenMeal))].Quantity - quantity);

        //        } else if(order.OrderItems[order.OrderItems.FindIndex(s => s.Meal.MealName.Equals(chosenMeal))].Quantity < quantity)
        //        {
        //            order.OrderItems[order.OrderItems.FindIndex(g => g.Meal.MealName.Equals(chosenMeal))].Price += MealsList.theList.Find(u => u.MealName.Equals(chosenMeal)).MealUnitPrice * (quantity - order.OrderItems[order.OrderItems.FindIndex(s => s.Meal.MealName.Equals(chosenMeal))].Quantity);
        //        } else if(order.OrderItems[order.OrderItems.FindIndex(s => s.Meal.MealName.Equals(chosenMeal))].Quantity == quantity)
        //        {

        //        }
        //        order.OrderItems[order.OrderItems.FindIndex(s => s.Meal.MealName.Equals(chosenMeal))].Quantity = quantity;

        //    }
        //    else if (order.OrderItems.Any(k => k.Meal.MealName.Equals(chosenMeal)))
        //    {
        //        order.OrderItems[order.OrderItems.FindIndex(s => s.Meal.MealName.Equals(chosenMeal))].Quantity += quantity;
        //        order.OrderItems[order.OrderItems.FindIndex(s => s.Meal.MealName.Equals(chosenMeal))].Price += MealsList.theList.Find(u => u.MealName.Equals(chosenMeal)).MealUnitPrice * quantity;
        //        order.OrderItems.Remove(order.OrderItems[order.OrderItems.FindIndex(w => w.Meal.MealName.Equals(replacedMeal))]);
        //    } 
        //    else
        //    {
        //        order.OrderItems[order.OrderItems.FindIndex(y => y.Meal.MealName.Equals(replacedMeal))].Meal = MealsList.theList.Find(z => z.MealName.Equals(chosenMeal));
        //        order.OrderItems[order.OrderItems.FindIndex(y => y.Meal.MealName.Equals(chosenMeal))].Price = MealsList.theList.Find(z => z.MealName.Equals(chosenMeal)).MealUnitPrice * quantity;
        //        order.OrderItems[order.OrderItems.FindIndex(y => y.Meal.MealName.Equals(chosenMeal))].Quantity = quantity;
        //    }
        //    double finalPrice = 0;
        //    foreach(var t in order.OrderItems)
        //    {
        //        finalPrice += t.Price;
        //    }
        //    order.TotalPrice = Math.Round((finalPrice), 2);
        //    currentList[currentList.FindIndex(x => x.OrderId == idOfOrder)] = order;
        //    Session["ListOfOrders"] = currentList;

        //    return RedirectToAction("Index", "Home");
        //}
    }
}