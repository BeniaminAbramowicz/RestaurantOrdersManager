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
        public ActionResult Index(string criteriaOption)
        {
            ListOfOrders resultList = new ListOfOrders();
            List<Order> listOfOrders = new List<Order> { };
            switch (criteriaOption)
            {
                case "all":
                    listOfOrders = (List<Order>)Session["listOfOrders"];     
                    resultList.OrdersList = listOfOrders;
                    return View(resultList);

                case "billpaid":
                    listOfOrders = ((List<Order>)Session["listOfOrders"]).FindAll(x => x.Status.Equals("BillPaid"));
                    resultList.OrdersList = listOfOrders;
                    return View(resultList);

                case "pendingpayment":
                    listOfOrders = ((List<Order>)Session["listOfOrders"]).FindAll(x => x.Status.Equals("PendingPayment"));
                    resultList.OrdersList = listOfOrders;
                    return View(resultList);

                case "1":
                    listOfOrders = ((List<Order>)Session["listOfOrders"]).FindAll(x => x.TableNumber == 1);
                    resultList.OrdersList = listOfOrders;
                    return View(resultList);

                case "2":
                    listOfOrders = ((List<Order>)Session["listOfOrders"]).FindAll(x => x.TableNumber == 2);
                    resultList.OrdersList = listOfOrders;
                    return View(resultList);

                case "3":
                    listOfOrders = ((List<Order>)Session["listOfOrders"]).FindAll(x => x.TableNumber == 3);
                    resultList.OrdersList = listOfOrders;
                    return View(resultList);
                
                case "4":
                    listOfOrders = ((List<Order>)Session["listOfOrders"]).FindAll(x => x.TableNumber == 4);
                    resultList.OrdersList = listOfOrders;
                    return View(resultList);

                case "5":
                    listOfOrders = ((List<Order>)Session["listOfOrders"]).FindAll(x => x.TableNumber == 5);
                    resultList.OrdersList = listOfOrders;
                    return View(resultList);

                default:
                    return View(resultList);
            }       
        }

        [HttpPost]
        [ActionName("DisplayOrders")]
        public ActionResult DisplayOrders(string criteriaOption)
        {
            return RedirectToAction("Index", criteriaOption);
        }

        public ActionResult AddOrder()
        {
            return View();
        }

    }
}