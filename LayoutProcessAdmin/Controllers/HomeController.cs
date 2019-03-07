﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayoutProcessAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["Logged"] == null)
            {
                return RedirectToAction("Login", "Users");
            }

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
    }
}