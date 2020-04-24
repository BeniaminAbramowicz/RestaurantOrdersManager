using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using ASPNETapp2.Models;
using ASPNETapp2.Facades;

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

        [HttpPut]
        public JsonResult UpdateOrderItem(int orderItemId, string orderData)
        {
            
            OrderItem orderItem = _restaurantFacade.FindOrderItemById(orderItemId).ResponseData;
            if(Regex.IsMatch(orderData, @"^[0-9]+$"))
            {
                orderItem.Quantity = Int32.Parse(orderData);
                orderItem.Price = orderItem.Meal.MealUnitPrice * orderItem.Quantity;
            }
            else
            {
                Meal meal = _restaurantFacade.FindMealByName(orderData).ResponseData;
                orderItem.Meal = meal;
                orderItem.Price = orderItem.Meal.MealUnitPrice * orderItem.Quantity;
            }
            OrderItem updatedOrder = _restaurantFacade.UpdateOrderItem(orderItem).ResponseData;
            double totalPrice = _restaurantFacade.FindOrderById(orderItem.OrderId).ResponseData.TotalPrice;
            return Json(new { Data = updatedOrder, TotalPrice = totalPrice });
        }
    }
}