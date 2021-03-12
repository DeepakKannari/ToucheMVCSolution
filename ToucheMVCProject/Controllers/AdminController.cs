using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToucheMVCProject.Models;

namespace ToucheMVCProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        toucheEntities dbContext = new toucheEntities();
        public ActionResult Index()
        {
            var result = dbContext.restaurants.Select(s => s);

            return View(result);
        }

        public ActionResult addRestaurant()
        {

            return View();
        }
        [HttpPost]
        public ActionResult addRestaurant(restaurantViewModel formvalues)
        {
            try
            {
                restaurant restaurantTuple = formvalues.getRestaurantValues();
                dbContext.restaurants.Add(restaurantTuple);
                dbContext.SaveChanges();
                return RedirectToAction("index");
            }
            catch(Exception ex)
            {
                ViewBag.exception = ex.Message;
            }
            return View();
        }

        public ActionResult addReservationinfo()
        {
            ReservationInfoViewModel ReservationInfoTuple = new ReservationInfoViewModel();
            ViewBag.timeSlot = ReservationInfoTuple.timeSlots;
            return View();
        }
        [HttpPost]
        public ActionResult addReservationinfo(ReservationInfoViewModel formValues)
        {
            try
            {
                reservationInfo reservationInfoTuple = formValues.getReservationInfoValues();
                dbContext.reservationInfoes.Add(reservationInfoTuple);
                dbContext.SaveChanges();
             
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                ViewBag.exception = ex.Message;
                return View();
            }
            

        }

        public ActionResult viewReservationInfo()
        {
            return View();
        }

        public ActionResult viewMenu(int id)
        {
            
            var result = dbContext.Menus.Where(s=>s.restaurantId.Equals(id));
            
            return View(result);
        }

        public ActionResult addMenuItems()
        {
            menuViewModel menuView = new menuViewModel();
            ViewBag.type = menuView.types;
            menuView.populateDropdownlists();
            ViewBag.ids = menuView.restaurantIds;
            return View();
        }
        [HttpPost]
        public ActionResult addMenuItems(menuViewModel formValues)
        {
            try
            {
                Menu menuItem = formValues.menuInfoValues();
                dbContext.Menus.Add(menuItem);
                dbContext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                ViewBag.exception = ex.Message;
                return View();
            }
        }

        public ActionResult EditValues( int id)
        {
            var result = dbContext.restaurants.SingleOrDefault(s=>s.id.Equals(id));

            if (result.status == "open")
            {
                result.status = "closed";
                dbContext.SaveChanges();
            }
            else
            {
                result.status = "open";
                dbContext.SaveChanges();
            }
            return RedirectToAction("index");
        }


    }
}