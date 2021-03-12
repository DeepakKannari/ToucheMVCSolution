using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToucheMVCProject.Models
{
    public class ReservationInfoViewModel
    {
        [Required]
        [Display(Name ="Resturant Id")]
        public int resturantId { get; set; }
        [Required]
        [Display(Name = "Time slot")]
        public string Timeslot { get; set; }
        [Required]
        [Display(Name = "Total seats")]
        public Nullable<int> TotalNoseats { get; set; }
        [Required]
        [Display(Name = "Available seats")]
        public Nullable<int> availableSeats { get; set; }

       public List<SelectListItem> timeSlots = new List<SelectListItem>()
        {
            new SelectListItem() { Text="9:30-12:30",Value="9:30-12:30"},
            new SelectListItem() { Text="12:30-15:30",Value="12:30-15:30"},
            new SelectListItem() { Text="15:30-18:30",Value="15:30-18:30"},
            new SelectListItem() { Text="18:30-21:30",Value="18:30-21:30"},

        };

        public reservationInfo getReservationInfoValues()
        {
            reservationInfo reservationInfoobj = new reservationInfo();
            reservationInfoobj.resturantId = this.resturantId;
            reservationInfoobj.Timeslot= this.Timeslot;
            reservationInfoobj.TotalNoseats = this.TotalNoseats;
            reservationInfoobj.availableSeats = this.availableSeats;
           

            return reservationInfoobj;
        }
    }
}