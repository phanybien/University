﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Home/

        //public string Index()
        //{
        //    return "Hello from Home";
        //}
    }
}
