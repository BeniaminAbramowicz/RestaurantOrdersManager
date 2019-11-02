using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNETapp2.Models;

namespace ASPNETapp2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ListOfOrders resultList = new ListOfOrders();
            List<Order> listOfOrders = new List<Order>();
            string chosenTable = (string)TempData["ChosenTable"];
            switch (chosenTable)
            {
                case "all":
                    if(Session["ListOfOrders"] != null)
                    {
                        listOfOrders = (List<Order>)Session["ListOfOrders"];
                        resultList.OrdersList = listOfOrders;
                    }                  
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Brak zamówień dla tego stolika";
                    }
                    return View(resultList);

                case "billpaid":
                    if (Session["ListOfOrders"] != null)
                    {
                        listOfOrders = ((List<Order>)Session["ListOfOrders"]).FindAll(x => x.Status.Equals(Order.OrderStatus.BillPaid));
                        resultList.OrdersList = listOfOrders;
                    }
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Brak zamówień dla tego stolika";
                    }
                    return View(resultList);

                case "pendingpayment":
                    if (Session["ListOfOrders"] != null)
                    {
                        listOfOrders = ((List<Order>)Session["ListOfOrders"]).FindAll(x => x.Status.Equals(Order.OrderStatus.PendingPayment));
                        resultList.OrdersList = listOfOrders;
                    }
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Brak zamówień dla tego stolika";
                    }
                    return View(resultList);

                case "1":
                    if (Session["ListOfOrders"] != null)
                    {
                        listOfOrders = ((List<Order>)Session["ListOfOrders"]).FindAll(x => x.TableNumber == 1);
                        resultList.OrdersList = listOfOrders;
                    }
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Brak zamówień dla tego stolika";
                    }
                    return View(resultList);

                case "2":
                    if (Session["ListOfOrders"] != null)
                    {
                        listOfOrders = ((List<Order>)Session["ListOfOrders"]).FindAll(x => x.TableNumber == 2);
                        resultList.OrdersList = listOfOrders;
                    }
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Brak zamówień dla tego stolika";
                    }
                    return View(resultList);

                case "3":
                    if (Session["ListOfOrders"] != null)
                    {
                        listOfOrders = ((List<Order>)Session["ListOfOrders"]).FindAll(x => x.TableNumber == 3);
                        resultList.OrdersList = listOfOrders;
                    }
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Brak zamówień dla tego stolika";
                    }
                    return View(resultList);

                case "4":
                    if (Session["ListOfOrders"] != null)
                    {
                        listOfOrders = ((List<Order>)Session["ListOfOrders"]).FindAll(x => x.TableNumber == 4);
                        resultList.OrdersList = listOfOrders;
                    }
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Brak zamówień dla tego stolika";
                    }
                    return View(resultList);

                case "5":
                    if (Session["ListOfOrders"] != null)
                    {
                        listOfOrders = ((List<Order>)Session["ListOfOrders"]).FindAll(x => x.TableNumber == 5);
                        resultList.OrdersList = listOfOrders;
                    }
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Brak zamówień dla tego stolika";
                    }
                    return View(resultList);

                default:
                    return View(resultList);
            }       
        }

        [HttpPost]
        [ActionName("DisplayOrders")]
        public ActionResult DisplayOrders(string chosenTable)
        {
            TempData["ChosenTable"] = chosenTable;
            return Redirect("Index");
        }
    }
}