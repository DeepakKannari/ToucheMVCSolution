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
        static string custId;
        static int orderId;
        static List<order> sessionOrders = new List<order>();

        // GET: Customer
        public ActionResult Index()
        {
            if (TempData.ContainsKey("custId"))
            {
                orderId = Convert.ToInt32(TempData.Peek("orderId"));
                restaurantViewModel restaurantView = new restaurantViewModel();
                restaurantView.populateLocation();
                ViewBag.locations = restaurantView.restaurantlocations;
                custId = TempData["custId"] as string;
                ViewBag.tester = custId;
                return View(dbContext.restaurants.ToList());
            }
            else
            {
                custId = null;
                return RedirectToAction("LogIn","LogIn");
            }
          
            
            return View(dbContext.restaurants.ToList());
        }

        public ActionResult searchform()
        {

            return View();
        }

        [HttpPost]
        public ActionResult orderLocationform(FormCollection formvalues)
        {
            return RedirectToAction("SearchFoodByLocation",new{location=formvalues[0]});
        }

        public ActionResult SearchFoodByLocation(string location)
        {
                TempData["location"] = location;
                var joinedTable = dbContext.restaurants.Join(dbContext.Menus,
                r => r.id,
                m => m.restaurantId,
                (r, m) => new { rname=r.name,rid = r.id, location = r.city,status=r.status,menuitemId= m.menuItemId,dishName= m.dishName, description= m.description,type= m.vtype,cuisinetype= m.cuisinetype,price= m.price });
            var result = joinedTable.Where(s=>s.location.Equals(location) && s.status.Equals("open")).Select(m=> new { rname=m.rname ,rid=m.rid,menuitemId=m.menuitemId, dishname=m.dishName,description=m.description,type=m.type,cuisinetype=m.cuisinetype,price=m.price});
            List<Menu> menuTable = new List<Menu>();
            
            foreach (var item in result)
            {
                Menu mentuple = new Menu();
                mentuple.menuItemId = item.menuitemId;
                mentuple.dishName = item.dishname;
                mentuple.description = item.description;
                mentuple.vtype= item.type;
                mentuple.cuisinetype = item.cuisinetype;
                mentuple.price = item.price;
                menuTable.Add(mentuple);
            }
            return View(menuTable);
        }
        [HttpPost]
        public ActionResult orderLocation(FormCollection formvalues)
        {
            return RedirectToAction("SearchFoodByLocation", new { location = formvalues[0] });
        }
        //public ActionResult filtredMenuView(string location)
        //{
        //    var result = dbContext.Where(s => s.city.Equals(location));
        //    return View(result);

        //}


        [HttpPost]
        public ActionResult dropdown(FormCollection formvalues) 
        {
            return RedirectToAction("filtredRestaurantsView", new { location = formvalues[0]});
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

        [HttpPost]
        public ActionResult orderFood(FormCollection fromvalue)
        {
            
            order ordertuple = new order();
            ordertuple.orderid = orderId++;
            ordertuple.custId = custId;
            ordertuple.quantity = Convert.ToInt32(fromvalue[0]);
            ordertuple.dishname = fromvalue[1];
            ordertuple.price = Convert.ToDouble(fromvalue[0]);
            sessionOrders.Add(ordertuple);
            //dbContext.orders.Add(ordertuple);
            //dbContext.SaveChanges();
            string location = TempData["location"] as string;
            return RedirectToAction("SearchFoodByLocation",new { location= location });
        }

        public ActionResult ViewCart()
        {
            double totalBill=0;
            int totalQuantity =0 ;
            foreach (var item in sessionOrders)
            {
                totalBill +=  Convert.ToDouble(item.price * item.quantity);
                totalQuantity += Convert.ToInt32(item.quantity);
            }
            ViewBag.totalBill = totalBill;
            ViewBag.totalQuantity = totalQuantity;
            return View(sessionOrders);
        }

        public JsonResult isSeatAvailable(int seats,int id,string timeslot)
        {
            var result = dbContext.reservationInfoes.Where(s=>s.resturantId.Equals(id)&& s.Timeslot.Equals(timeslot)).Any(s => s.availableSeats > seats);
            return Json(!result,JsonRequestBehavior.AllowGet);
        }

       

    }
}