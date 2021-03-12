using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToucheMVCProject.Models;

namespace ToucheMVCProject.Controllers
{
    public class CustomerController : Controller
    {
        toucheEntities dbContext = new toucheEntities();

        // GET: Customer
        public ActionResult Index()
        {
            restaurantViewModel restaurantView = new restaurantViewModel();
            restaurantView.populateLocation();
            ViewBag.locations = restaurantView.restaurantlocations;
            
            return View();
        }
    }
}