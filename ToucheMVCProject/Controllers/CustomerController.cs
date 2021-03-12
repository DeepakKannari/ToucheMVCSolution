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
            
            return View(dbContext.restaurants.ToList());
        }

        public ActionResult SearchFoodByLocation(string location)
        {
            //var joinresult = dbContext.bugs.Join(dbContext.project_modules,
            //    b => b.moduleId,
            //    p => p.id,
            //    (b, p) => new { b.id, b.moduleId, b.BugStatus, b.BugDescription, p.developer });
            //var result = joinresult.Where(s => s.developer.Equals(developerId)).Select(b => new { id = b.id, moduleid = b.moduleId, bugstatus = b.BugStatus, bugdescription = b.BugDescription });
            var joinedTable = dbContext.restaurants.Join(dbContext.Menus,
                r => r.id,
                m => m.restaurantId,
                (r, m) => new { r.id,r.city,r.status, m.menuItemId, m.dishName, m.description, m.vtype, m.cuisinetype, m.price });
            var result = joinedTable.Where(s=>s.city.Equals(location) && s.status.Equals("open")).Select(m=> new { rid=m.id,menuitemId=m.menuItemId,dishname=m.dishName,description=m.description,vtype=m.vtype,cuisinetype=m.cuisinetype,price=m.price});
            List<Menu> bugtable = new List<Menu>();
            
            foreach (var item in result)
            {
                Menu mentuple = new Menu();
                mentuple.menuItemId = item.menuitemId;
                mentuple.dishName = item.dishname;
                mentuple.description = item.description;
                mentuple.vtype= item.vtype;
                mentuple.cuisinetype = item.cuisinetype;
                mentuple.price = item.price;
            }
            return View(dbContext.restaurants.ToList());
        }
        [HttpPost]
        public ActionResult orderLocation(FormCollection formvalues)
        {
            return RedirectToAction("SearchFoodByLocation", new { location = formvalues[0] });
        }
        public ActionResult filtredMenuView(string location)
        {
            var result = dbContext.Where(s => s.city.Equals(location));
            return View(result);

        }


        [HttpPost]
        public ActionResult dropdown(FormCollection formvalues) 
        {
            return RedirectToAction("filtredView", new { location = formvalues[0]});
        }

        public ActionResult filtredRestaurantsView(string location)
        {
           var result = dbContext.restaurants.Where(s => s.city.Equals(location)&& s.status.Equals("open"));
            return View(result);
            
        }

        public ActionResult ReserverTable()
        {
            reservationViewModel reservationView = new reservationViewModel();
            ViewBag.timeSlots = reservationView.timeSlots;

            return View();
        }

        public JsonResult isSeatAvailable(int seats,int id,string timeslot)
        {
            var result = dbContext.reservationInfoes.Where(s=>s.resturantId.Equals(id)&& s.Timeslot.Equals(timeslot)).Any(s => s.availableSeats > seats);
            return Json(!result,JsonRequestBehavior.AllowGet);
        }

        public  orderfood()

    }
}