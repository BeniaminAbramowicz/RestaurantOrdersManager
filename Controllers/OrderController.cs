using System;
using System.Collections.Generic;
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
                MealsList = _restaurantFacade.FindAllMeals(new SearchCondition("")).ToList(),
                TablesList = _restaurantFacade.FindAllTables(new SearchCondition("")).ToList()
            };
            return View(listOfMealsAndTables);
        }

        [HttpPost]
        public JsonResult CreateOrder(OrderDTO receivedOrder)
        {
            double totalPrice = 0;
            Table chosenTable = _restaurantFacade.FindTableById(receivedOrder.TableNumber);
            List<Meal> mealsList = new List<Meal>();
            List<OrderItem> orderItems = new List<OrderItem>();
            Meal currentMeal;
            foreach (var sentOrderItem in receivedOrder.OrderItems)
            {
                currentMeal = _restaurantFacade.FindMealByName(sentOrderItem.MealName);
                mealsList.Add(currentMeal);
                totalPrice += currentMeal.MealUnitPrice * sentOrderItem.Quantity;
            }
            for(var i = 0; i < receivedOrder.OrderItems.Count; i++)
            {
                orderItems.Add(new OrderItem(mealsList[i], receivedOrder.OrderItems[i].Quantity, mealsList[i].MealUnitPrice * receivedOrder.OrderItems[i].Quantity));
            }
            _restaurantFacade.AddOrder(new Order(orderItems, totalPrice, chosenTable, Order.OrderStatus.PendingPayment));
            return Json("");
        }

       //[HttpPost]
       //public JsonResult RemoveOrder(int idOfOrder)
       // {
       //     List<Order> currentList = (List<Order>)Session["ListOfOrders"];
       //     currentList.Remove(currentList.Find(x => x.OrderId == idOfOrder));
       //     Session["ListOfOrders"] = currentList;
       //     return Json("");
       // }

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

        //[HttpPost]
        //public JsonResult AddToDisplay(MealItemDTO mealItem, int idOfOrder)
        //{
        //    string chosenTable = (string)TempData["ChosenTable"];
        //    Meal theMeal = MealsList.theList.Find(x => x.MealId == mealItem.MealItemId);
        //    List<Order> listOfOrders = (List<Order>)Session["ListOfOrders"];
        //    Order order = listOfOrders.Find(y => y.OrderId == idOfOrder);
        //    if(order.OrderItems.Any(k => k.Meal.MealId == mealItem.MealItemId))
        //    {
        //        order.OrderItems.Find(j => j.Meal.MealId == mealItem.MealItemId).Quantity += mealItem.MealQuantity;
        //        order.OrderItems.Find(j => j.Meal.MealId == mealItem.MealItemId).Price += MealsList.theList.Find(l => l.MealId == mealItem.MealItemId).MealUnitPrice * mealItem.MealQuantity;
        //    } else
        //    {
        //        order.OrderItems.Add(new OrderItem(theMeal, mealItem.MealQuantity, theMeal.MealUnitPrice * mealItem.MealQuantity));
        //    }
        //    order.TotalPrice += (theMeal.MealUnitPrice * mealItem.MealQuantity);
        //    listOfOrders[listOfOrders.FindIndex(z => z.OrderId == idOfOrder)] = order;
        //    Session["ListOfOrders"] = listOfOrders;
        //    TempData["ChosenTable"] = chosenTable;
        //    return Json("");
        //}


        //[HttpPost]
        //public JsonResult RemovePosition(int idOfOrder, string itemName)
        //{
        //    List<Order> currentList = (List<Order>)Session["ListOfOrders"];
        //    Order order = currentList.Find(x => x.OrderId == idOfOrder);
        //    order.TotalPrice = Math.Round(order.TotalPrice - order.OrderItems[order.OrderItems.FindIndex(z => z.Meal.MealName.Equals(itemName))].Price, 2);
        //    order.OrderItems.Remove(order.OrderItems.Find(y => y.Meal.MealName.Equals(itemName)));
        //    currentList[currentList.FindIndex(x => x.OrderId == idOfOrder)] = order;
        //    Session["ListOfOrders"] = currentList;
        //    var data = new { data = order.TotalPrice };
        //    return Json(data);
        //}

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