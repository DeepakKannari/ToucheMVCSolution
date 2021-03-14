﻿using System;
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
            try
            {
                if (TempData.ContainsKey("custId"))
                {
                    orderId = Convert.ToInt32(TempData.Peek("orderId"));
                    restaurantViewModel restaurantView = new restaurantViewModel();
                    restaurantView.populateLocation();
                    ViewBag.locations = restaurantView.restaurantlocations;
                    custId = TempData.Peek("custId") as string;
                    ViewBag.tester = custId;
                    return View(dbContext.restaurants.ToList());
                }
                else
                {
                    custId = null;
                    return RedirectToAction("LogIn", "LogIn");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View(dbContext.restaurants.ToList());
            }
                     
            
        }
        //searchform -Renders the
        //form for filtering food
        //by location.
        public ActionResult searchform()
        {
            if (TempData.ContainsKey("custId"))
            {

                return View();
            }
            else{
                custId = null;
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        //orderLocationform - Post method for 
        //search food by location
        [HttpPost]
        public ActionResult orderLocationform(FormCollection formvalues)
        {
            if (TempData.ContainsKey("custId"))
            {
                TempData["location"] = formvalues[0];
                return RedirectToAction("SearchFoodByLocation"/*, new { location = formvalues[0] }*/);
            }
            else
            {
                custId = null;
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        //SearchFoodByLocation- Filter Food
        //by location and display the table of menu items 
        public ActionResult SearchFoodByLocation(/*string locatio*/)
        {
            string location = TempData.Peek("location") as string;
            try
            {
                if (TempData.ContainsKey("custId"))
                {
                    //TempData["location"] = location;
                    var joinedTable = dbContext.restaurants.Join(dbContext.Menus,
                    r => r.id,
                    m => m.restaurantId,
                    (r, m) => new { rname = r.name, rid = r.id, location = r.city, status = r.status, menuitemId = m.menuItemId, dishName = m.dishName, description = m.description, type = m.vtype, cuisinetype = m.cuisinetype, price = m.price });
                    var result = joinedTable.Where(s => s.location.Equals(location) && s.status.Equals("open")).Select(m => new { rname = m.rname, rid = m.rid, menuitemId = m.menuitemId, dishname = m.dishName, description = m.description, type = m.type, cuisinetype = m.cuisinetype, price = m.price });
                    List<Menu> menuTable = new List<Menu>();

                    foreach (var item in result)
                    {
                        Menu mentuple = new Menu();
                        mentuple.menuItemId = item.menuitemId;
                        mentuple.dishName = item.dishname;
                        mentuple.description = item.description;
                        mentuple.vtype = item.type;
                        mentuple.cuisinetype = item.cuisinetype;
                        mentuple.price = item.price;
                        menuTable.Add(mentuple);
                    }
                    return View(menuTable);
                }
                else
                {
                    custId = null;
                    return RedirectToAction("LogIn", "LogIn");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult orderLocation(FormCollection formvalues)
        {
            if (TempData.ContainsKey("custId"))
            {
                TempData["location"] = formvalues[0];

                return RedirectToAction("SearchFoodByLocation"/*, new { location = formvalues[0] }*/);
            }
            else
            {
                custId = null;
                return RedirectToAction("LogIn", "LogIn");
            }
        }
        //public ActionResult filtredMenuView(string location)
        //{
        //    var result = dbContext.Where(s => s.city.Equals(location));
        //    return View(result);

        //}

        //dropdown- flters resturant by location
        [HttpPost]
        public ActionResult resturantsearchLocation(FormCollection formvalues) 
        {
            if (TempData.ContainsKey("custId"))
            {
                return RedirectToAction("filtredRestaurantsView", new { location = formvalues[0]});
            }
            else
            {
                custId = null;
                return RedirectToAction("LogIn", "LogIn");
            }
        }

        public ActionResult filtredRestaurantsView(string location)
        {
            try
            {
                if (TempData.ContainsKey("custId"))
                {
                    var result = dbContext.restaurants.Where(s => s.city.Equals(location) && s.status.Equals("open"));
                    return View(result);

                }
                else
                {
                    custId = null;
                    return RedirectToAction("LogIn", "LogIn");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();

            }
            
        }
        // reserve table  
        public ActionResult ReserverTable(int id)
        {
            try
            {
                //if (TempData.ContainsKey("custId"))
                //{
                    reservationViewModel reservationView = new reservationViewModel();
                     ViewBag.timeSlots = reservationView.populateTimeSlot(id);
                
                     //reservationView.restaurantId = id;
                     ViewBag.restaurantId = id+"";

                     return View();
                //}
                //else
                //{
                //    custId = null;
                //    return RedirectToAction("LogIn", "LogIn");
                //}
            }
            catch(Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ReserverTable(reservationViewModel formvalues)
        {
            int lastReservationId;
            try
            {
                if (dbContext.reservations.Any())
                {
                    lastReservationId = dbContext.reservations.Select(s => s.Id).Max();
                }
                else
                {
                    lastReservationId = 0;
                }
                reservationViewModel reservationView = new reservationViewModel();
                //ViewBag.timeSlots = reservationView.populateTimeSlot(id);
                reservation reservationTuple = new reservation();
                reservationTuple.customerId = TempData.Peek("custId") as string;
                reservationTuple.Id = ++lastReservationId;
                reservationTuple.restaurantId = formvalues.restaurantId;
                reservationTuple.noOfPeople = formvalues.noOfPeople;
                reservationTuple.timeslot = formvalues.timeslot;
                var result = dbContext.reservationInfoes.SingleOrDefault(s=>s.resturantId.Equals(formvalues.restaurantId)&&s.Timeslot.Equals(formvalues.timeslot));
                if (result != null)
                {
                    
                }

                return View();
                
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult orderFood(FormCollection fromvalue)
        {
            try
            {
                if (TempData.ContainsKey("custId"))
                {
                    order ordertuple = new order();
                    ordertuple.orderid = orderId++;
                    ordertuple.custId = custId;
                    ordertuple.quantity = Convert.ToInt32(fromvalue[0]);
                    ordertuple.dishname = fromvalue[1];
                    ordertuple.price = Convert.ToDouble(fromvalue[2]);
                    sessionOrders.Add(ordertuple);
                    //dbContext.orders.Add(ordertuple);
                    //dbContext.SaveChanges();
                    string location = TempData.Peek("location") as string;
                    return RedirectToAction("SearchFoodByLocation", new { location = location });
                }
                else 
                {
                    custId = null;
                    return RedirectToAction("LogIn", "LogIn");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }
        }

        public ActionResult ViewCart()
        {
            try
            {
                if (TempData.ContainsKey("custId"))
                {
                    double totalBill = 0;
                int totalQuantity = 0;
                foreach (var item in sessionOrders)
                {
                    totalBill += Convert.ToDouble(item.price * item.quantity);
                    totalQuantity += Convert.ToInt32(item.quantity);
                }
                ViewBag.totalBill = totalBill;
                ViewBag.totalQuantity = totalQuantity;
                return View(sessionOrders);
                }
                else
                {
                    custId = null;
                    return RedirectToAction("LogIn", "LogIn");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }
        }

        public ActionResult placeOrder()
        {
            try
            {
                if (TempData.ContainsKey("custId"))
                {
                    dbContext.orders.AddRange(sessionOrders);
                    dbContext.SaveChanges();
                    //sessionOrders.Clear();
                    //ViewBag.ordermessage = "Your's Order has been placed";
                    return RedirectToAction("ViewCart");
                }
                else
                {
                    custId = null;
                    return RedirectToAction("LogIn", "LogIn");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }
        }

        public ActionResult filterbyVeg()
        {
            string location = TempData.Peek("location") as string;
            try
            {
                if (TempData.ContainsKey("custId"))
                {
                    //TempData["location"] = location;
                    var joinedTable = dbContext.restaurants.Join(dbContext.Menus,
                    r => r.id,
                    m => m.restaurantId,
                    (r, m) => new { rname = r.name, rid = r.id, location = r.city, status = r.status, menuitemId = m.menuItemId, dishName = m.dishName, description = m.description, type = m.vtype, cuisinetype = m.cuisinetype, price = m.price });
                    var result = joinedTable.Where(s => s.location.Equals(location) && s.status.Equals("open") && s.type.Equals("veg")).Select(m => new { rname = m.rname, rid = m.rid, menuitemId = m.menuitemId, dishname = m.dishName, description = m.description, type = m.type, cuisinetype = m.cuisinetype, price = m.price });
                    List<Menu> menuTable = new List<Menu>();

                    foreach (var item in result)
                    {
                        Menu mentuple = new Menu();
                        mentuple.menuItemId = item.menuitemId;
                        mentuple.dishName = item.dishname;
                        mentuple.description = item.description;
                        mentuple.vtype = item.type;
                        mentuple.cuisinetype = item.cuisinetype;
                        mentuple.price = item.price;
                        menuTable.Add(mentuple);
                    }
                    return View(menuTable);
                }
                else
                {
                    custId = null;
                    return RedirectToAction("LogIn", "LogIn");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }

            
            
        }

        public ActionResult filterbyNonVeg()
        {
            string location = TempData.Peek("location") as string;
            try
            {
                if (TempData.ContainsKey("custId"))
                {
                    //TempData["location"] = location;
                    var joinedTable = dbContext.restaurants.Join(dbContext.Menus,
                    r => r.id,
                    m => m.restaurantId,
                    (r, m) => new { rname = r.name, rid = r.id, location = r.city, status = r.status, menuitemId = m.menuItemId, dishName = m.dishName, description = m.description, type = m.vtype, cuisinetype = m.cuisinetype, price = m.price });
                    var result = joinedTable.Where(s => s.location.Equals(location) && s.status.Equals("open") && s.type.Equals("non-veg")).Select(m => new { rname = m.rname, rid = m.rid, menuitemId = m.menuitemId, dishname = m.dishName, description = m.description, type = m.type, cuisinetype = m.cuisinetype, price = m.price });
                    List<Menu> menuTable = new List<Menu>();

                    foreach (var item in result)
                    {
                        Menu mentuple = new Menu();
                        mentuple.menuItemId = item.menuitemId;
                        mentuple.dishName = item.dishname;
                        mentuple.description = item.description;
                        mentuple.vtype = item.type;
                        mentuple.cuisinetype = item.cuisinetype;
                        mentuple.price = item.price;
                        menuTable.Add(mentuple);
                    }
                    return View(menuTable);
                }
                else
                {
                    custId = null;
                    return RedirectToAction("LogIn", "LogIn");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }



        }

        public ActionResult filterbyPrice()
        {
            string location = TempData.Peek("location") as string;
            try
            {
                if (TempData.ContainsKey("custId"))
                {
                    //TempData["location"] = location;
                    var joinedTable = dbContext.restaurants.Join(dbContext.Menus,
                    r => r.id,
                    m => m.restaurantId,
                    (r, m) => new { rname = r.name, rid = r.id, location = r.city, status = r.status, menuitemId = m.menuItemId, dishName = m.dishName, description = m.description, type = m.vtype, cuisinetype = m.cuisinetype, price = m.price });
                    var result = joinedTable.Where(s => s.location.Equals(location) && s.status.Equals("open")).Select(m => new { rname = m.rname, rid = m.rid, menuitemId = m.menuitemId, dishname = m.dishName, description = m.description, type = m.type, cuisinetype = m.cuisinetype, price = m.price }).OrderBy(m=>m.price);
                    List<Menu> menuTable = new List<Menu>();

                    foreach (var item in result)
                    {
                        Menu mentuple = new Menu();
                        mentuple.menuItemId = item.menuitemId;
                        mentuple.dishName = item.dishname;
                        mentuple.description = item.description;
                        mentuple.vtype = item.type;
                        mentuple.cuisinetype = item.cuisinetype;
                        mentuple.price = item.price;
                        menuTable.Add(mentuple);
                    }
                    return View(menuTable);
                }
                else
                {
                    custId = null;
                    return RedirectToAction("LogIn", "LogIn");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }



        }

        public JsonResult isSeatAvailable(int noOfPeople, int restaurantId, string timeslot)
        {
            var result = dbContext.reservationInfoes.Where(s=>s.resturantId.Equals(restaurantId) && s.Timeslot.Equals(timeslot)).Any(s => s.availableSeats >= noOfPeople);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

       

    }
}