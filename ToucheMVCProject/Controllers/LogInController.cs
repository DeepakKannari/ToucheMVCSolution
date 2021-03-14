using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToucheMVCProject.Models;

namespace ToucheMVCProject.Controllers
{
    public class LogInController : Controller
    {
        toucheEntities dbContext = new toucheEntities();
        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(RegistrationViewModel userlogin)
        {
            var result = dbContext.registrations.FirstOrDefault(log=>log.userId.Equals(userlogin.userId) && (log.password.Equals(userlogin.password)));
            if (result != null)
            {
                switch (result.role)
                {
                    case "customer": if (dbContext.orders.Any(ord => ord.custId.Equals(result.userId)))
                        {
                            var maxvalue = dbContext.orders.Where(ord => ord.custId.Equals(result.userId)).Select(s => s.orderid).Max();
                            TempData["orderId"] = ++ maxvalue;
                        }
                        else
                        {
                            TempData["orderId"] = 1;
                        }
                        /*var maxvalue = dbContext.orders.Where(ord => ord.custId.Equals(result.userId)).Select(s => s.orderid).Max();*/
                        TempData["custId"] = result.userId;
                        return RedirectToAction("Index", "Customer");
                    case "admin":
                        TempData["adminId"] = result.userId;
                        return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }


        //[HttpPost]
        //public ActionResult LogIn(/*LogIn loginDetails*/)
        //{
        //    //LogIn result = dbContext.LogIns.FirstOrDefault(log => log.userId.Equals(loginDetails.userId) && (log.password.Equals(loginDetails.password)));
        //    //if (result != null)
        //    //{
        //    //    switch (result.role)
        //    //    {
        //    //        case "manager": return RedirectToAction("viewProject", "Manager");
        //    //        case "teamlead": return RedirectToAction("viewModule", "TeamLead");
        //    //        case "developer":
        //    //            TempData["developer"] = result.userId;
        //    //            return RedirectToAction("viewModule", "developer");
        //    //        case "tester":
        //    //            TempData["testerId"] = result.userId;
        //    //            return RedirectToAction("viewModule", "Tester");
        //    //    }
        //    }
        //    return View();
        //}

        public ActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel CustomerValues)
        {
            registration newUser = new registration();
            Customer newCustomer = new Customer();
            address deliveryaddress = new address();
            newUser.userId = CustomerValues.userId;
            newUser.username = CustomerValues.username;
            newUser.password = CustomerValues.password;
            newUser.role = "customer";
            dbContext.registrations.Add(newUser);
            dbContext.SaveChanges();
            newCustomer.custId = CustomerValues.userId;
            newCustomer.name = CustomerValues.username;
            newCustomer.phoneNo = CustomerValues.phoneNo;
            dbContext.Customers.Add(newCustomer);
            dbContext.SaveChanges();
            deliveryaddress.custId = CustomerValues.userId;
            deliveryaddress.deliveraddress = CustomerValues.address;
            dbContext.addresses.Add(deliveryaddress);
            dbContext.SaveChanges();


            //dbContext.registrations.Add();
            return RedirectToAction("LogIn");
        }
    }
}