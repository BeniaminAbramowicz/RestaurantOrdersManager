using System.Collections.Generic;
using System;
using System.Web.Mvc;
using ASPNETapp2.Models;

namespace ASPNETapp2.Controllers
{
    public class TestController : Controller
    {
        
        
        public ActionResult Index()
        {

            //IEnumerable<UserTestModel> usersList = EntityMapper.QueryForList<UserTestModel>("GetUsersList", "");
            //UserTestDAO testModel = new UserTestDAO
            //{
            //    UserList = usersList
            //};
            return View();
        }
    }
}