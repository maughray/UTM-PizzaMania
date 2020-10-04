using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using PizzaMania.Models;

namespace PizzaMania.Controllers
{
    public class MainHomeController : Controller
    {
        private Context context = new Context();

        public ActionResult Index()
        {
            return View();
        }
    }
}