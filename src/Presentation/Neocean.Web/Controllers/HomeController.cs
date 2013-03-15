using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Neocean.Services.Customer;
using Neocean.DataObjects;

namespace Neocean.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "欢迎使用 ASP.NET MVC!";
           
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserDataObject user)
        {
            user.ID = Guid.NewGuid();

            var userService = DependencyResolver.Current.GetService<IUserService>();
            userService.InsertUser(new List<UserDataObject>() { user });
            return RedirectToAction("SaveUser"); 
        }

        public ActionResult SaveUser()
        {
            ViewBag.Message = "用户保存成功!";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
