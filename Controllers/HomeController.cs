using DASH_BOOKING.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DASH_BOOKING.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(string txtUsername, string txtPassword)
        {
            if (IsValidAdmin(txtUsername, txtPassword))
            {
                // Admin credentials are valid, redirect to admin dashboard or another page
                return RedirectToAction("Admin", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid username or password";
                return View("Login"); // Redirect back to the login page with an error message
            }
        }
        private bool IsValidAdmin(string username, string password)
        {
            // Implement your logic to validate admin credentials here
            // This could involve querying your database or any other validation method
            // For demonstration purposes, I'll just check hardcoded credentials
            return username == "admin" && password == "1234";
        }
    }
}