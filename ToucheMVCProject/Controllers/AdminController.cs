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
            if (TempData.ContainsKey("adminId"))
            {
                var result = dbContext.restaurants.Select(s => s);

                return View(result);
            }
            else
            {
                TempData.Clear();
               return  RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult addRestaurant()
        {
            if (TempData.ContainsKey("adminId"))
            {
                restaurantViewModel restaurantView = new restaurantViewModel();
                ViewBag.status = restaurantView.listOfStatus;

                return View();
            }
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        [HttpPost]
        public ActionResult addRestaurant(restaurantViewModel formvalues)
        {
            if (TempData.ContainsKey("adminId"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        restaurantViewModel restaurantView = new restaurantViewModel();
                        ViewBag.status = restaurantView.listOfStatus;
                        restaurant restaurantTuple = formvalues.getRestaurantValues();
                        dbContext.restaurants.Add(restaurantTuple);
                        dbContext.SaveChanges();
                        return RedirectToAction("index");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.exception = ex.Message;
                    }
                    return View();
                }
                else
                {
                    return View();
                }
            }
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult addReservationinfo(int id)
        {
            if (TempData.ContainsKey("adminId"))
            {
                ReservationInfoViewModel ReservationInfoTuple = new ReservationInfoViewModel();
                ViewBag.timeSlot = ReservationInfoTuple.timeSlots;
                ViewBag.restaurantId = id;
                return View();
            }
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        [HttpPost]
        public ActionResult addReservationinfo(ReservationInfoViewModel formValues)
        {
            if (TempData.ContainsKey("adminId"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        ReservationInfoViewModel ReservationInfoTuple = new ReservationInfoViewModel();
                        ViewBag.timeSlot = ReservationInfoTuple.timeSlots;
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
                else
                {
                    return View();
                }
            }
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }


        }

        public ActionResult viewReservationInfo()
        {
            if (TempData.ContainsKey("adminId"))
            {
                return View();
            }
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult viewMenu(int id)
        {

            if (TempData.ContainsKey("adminId"))
            {
                var result = dbContext.Menus.Where(s => s.restaurantId.Equals(id));

                return View(result);
            }
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult addMenuItems()
        {
            if (TempData.ContainsKey("adminId"))
            {
                menuViewModel menuView = new menuViewModel();
                ViewBag.type = menuView.types;
                menuView.populateDropdownlists();
                ViewBag.ids = menuView.restaurantIds;
                return View();
            }
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        [HttpPost]
        public ActionResult addMenuItems(menuViewModel formValues)
        {
            if (TempData.ContainsKey("adminId"))
            {
                try
                {
                    
                        menuViewModel menuView = new menuViewModel();
                        ViewBag.type = menuView.types;
                        menuView.populateDropdownlists();
                        ViewBag.ids = menuView.restaurantIds;
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
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult EditValues(int id)
        {
            if (TempData.ContainsKey("adminId"))
            {
                var result = dbContext.restaurants.SingleOrDefault(s => s.id.Equals(id));

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
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult viewReservation()
        {
            if (TempData.ContainsKey("adminId"))
            {
                var joinedTable = dbContext.reservations.Join(dbContext.restaurants,
                re => re.restaurantId,
                r => r.id,
                (re, r) => new { restaurantname = r.name, id = re.Id, noofpeople = re.noOfPeople, customerid = re.customerId, restaurantid = re.restaurantId, timeslot = re.timeslot });
                var result = joinedTable.Select(s => s);
                List<reservationViewModel> reservations = new List<reservationViewModel>();
                foreach (var item in result)
                {
                    reservationViewModel reservation = new reservationViewModel();
                    reservation.Id = item.id;
                    reservation.noOfPeople = item.noofpeople;
                    reservation.restaurantId = item.restaurantid;
                    reservation.restaurantName = item.restaurantname;
                    reservation.timeslot = item.timeslot;
                    reservation.customerId = item.customerid;

                    reservations.Add(reservation);
                }
                return View(reservations);
                //var reservationTable = dbContext.reservations.Select(s => s);
                //return View(reservationTable);
            }
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public ActionResult clearReservation(int id)
        {
            if (TempData.ContainsKey("adminId"))
            {
                var reservationTuple = dbContext.reservations.SingleOrDefault(s=>s.Id.Equals(id));
            clearedreservation clearReservation = new clearedreservation();
            int restaurantId;
            


            if (reservationTuple != null)
            {
                clearReservation.Id = reservationTuple.Id;
                clearReservation.restaurantId = reservationTuple.restaurantId;
                clearReservation.noOfPeople = reservationTuple.noOfPeople;
                clearReservation.timeslot = reservationTuple.timeslot;
                clearReservation.customerId = reservationTuple.customerId;
                restaurantId= Convert.ToInt32(reservationTuple.restaurantId);
                
                dbContext.clearedreservations.Add(clearReservation);
                dbContext.SaveChanges();
                var reservationInfo = dbContext.reservationInfoes.SingleOrDefault(s => s.resturantId.Equals(restaurantId) && s.Timeslot.Equals(reservationTuple.timeslot));
                if (reservationInfo != null)
                {
                    reservationInfo.availableSeats+= reservationTuple.noOfPeople;
                    dbContext.SaveChanges();
                }
                
                dbContext.reservations.Remove(reservationTuple);
                dbContext.SaveChanges();

                return RedirectToAction("viewReservation");
            }
            }
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }

            return View();
        }

        public ActionResult viewOrders()
        {
            if (TempData.ContainsKey("adminId"))
            {
                var result = dbContext.orders.Select(s => s);

                return View(result);
            }
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        public ActionResult clearOrder(string customerid, int orderid)
        {
            if (TempData.ContainsKey("adminId"))
            {
                var ordertuple = dbContext.orders.SingleOrDefault(s => s.orderid.Equals(orderid) && s.custId.Equals(customerid));
            delivered deliveredtuple = new delivered();
            if (ordertuple != null)
            {
                deliveredtuple.custid = ordertuple.custId;
                deliveredtuple.orderId = ordertuple.orderid;
                deliveredtuple.dishname = ordertuple.dishname;
                deliveredtuple.price = ordertuple.price;
                deliveredtuple.quantity = ordertuple.quantity;

                dbContext.delivereds.Add(deliveredtuple);
                    dbContext.SaveChanges();
                dbContext.orders.Remove(ordertuple);
                dbContext.SaveChanges();
                return RedirectToAction("viewOrders");
            }
            return View();
            }
            else
            {
                TempData.Clear();
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult logout()
        {
            TempData.Clear();

            return RedirectToAction("LogIn","LogIn");
        }


    }
}