using AppDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppDemo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model) {
            if (model.Email == "admin@awzm.com")
            {
                System.Web.HttpContext.Current.Session["AuthUser"] = "admin@awzm.com";
                System.Web.HttpContext.Current.Session["AuthUserID"] = "1";
                System.Web.HttpContext.Current.Session["AuthUserName"] = "admin admin";
                ViewBag.email = model.Email;
                return RedirectToAction("Dashboard");
            }
            else {
                ViewBag.message = "Enter Correct email";
                return RedirectToAction( "Index", "Home");
            }
        }

        public ActionResult Dashboard(string email) {
            ViewBag.email = email;
            return View();
        }
        public ActionResult Logout(string email) {
            //perform logout works
            return RedirectToAction("Index","Home");
        }
    }
}