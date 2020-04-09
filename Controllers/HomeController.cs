﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                case "empty":
                    if(Session["ListOfOrders"] != null)
                    {
                        listOfOrders = (List<Order>)Session["ListOfOrders"];
                        resultList.OrdersList = listOfOrders;
                    }                  
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Lista zamówień jest pusta";
                    }
                    TempData["ChosenTable"] = chosenTable;
                    return View(resultList);

                case "billpaid":
                    if (Session["ListOfOrders"] != null)
                    {
                        listOfOrders = ((List<Order>)Session["ListOfOrders"]).FindAll(x => x.Status.Equals(Order.OrderStatus.BillPaid));
                        resultList.OrdersList = listOfOrders;
                    }
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Brak zamówień z zapłaconym rachunkiem";
                    }
                    TempData["ChosenTable"] = chosenTable;
                    return View(resultList);

                case "pendingpayment":
                    if (Session["ListOfOrders"] != null)
                    {
                        listOfOrders = ((List<Order>)Session["ListOfOrders"]).FindAll(x => x.Status.Equals(Order.OrderStatus.PendingPayment));
                        resultList.OrdersList = listOfOrders;
                    }
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Brak zamówień oczekujących na zapłatę";
                    }
                    TempData["ChosenTable"] = chosenTable;
                    return View(resultList);

                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                    if (Session["ListOfOrders"] != null)
                    {
                        listOfOrders = ((List<Order>)Session["ListOfOrders"]).FindAll(x => x.TableNumber ==  Int32.Parse(chosenTable));
                        resultList.OrdersList = listOfOrders;
                    }
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Brak zamówień dla tego stolika";
                    }
                    TempData["ChosenTable"] = chosenTable;
                    return View(resultList);

                default:
                    if (!listOfOrders.Any())
                    {
                        TempData["error"] = "Lista zamówień jest pusta";
                    }
                    return View(resultList);
            }       
        }

        [HttpPost]
        public ActionResult DisplayOrders(string chosenTable)
        {
            TempData["ChosenTable"] = chosenTable;
            return Redirect("Index");
        }
    }
}