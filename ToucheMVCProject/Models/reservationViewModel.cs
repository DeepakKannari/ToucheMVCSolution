using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToucheMVCProject.Models
{
    public class reservationViewModel
    {
        toucheEntities dbContext = new toucheEntities();

        [Required]
        [Display(Name = "Reservation Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Customer Id")]
        public string customerId { get; set; }
        [Required]
        //[Display(Name = "Restaurant Id")]
        public Nullable<int> restaurantId { get; set; }
        [Required]
        //[Display(Name = "Time Slot")]
        public string timeslot { get; set; }
        [Required]
        //[Display(Name = "No. of people")]
        [Remote("isSeatAvailable", "Customer", AdditionalFields = "restaurantId,timeslot", ErrorMessage ="that many seats are not available")]
        public Nullable<int> noOfPeople { get; set; }

        public List<SelectListItem> timeSlots = new List<SelectListItem>();
        //{
        //    new SelectListItem() { Text="9:30-12:30",Value="9:30-12:30"},
        //    new SelectListItem() { Text="12:30-15:30",Value="12:30-15:30"},
        //    new SelectListItem() { Text="15:30-18:30",Value="15:30-18:30"},
        //    new SelectListItem() { Text="18:30-21:30",Value="18:30-21:30"},

        //};
        public List<SelectListItem> populateTimeSlot(int id)
        {
           var result = dbContext.reservationInfoes.Where(s => s.resturantId.Equals(id)).Select(s=>s.Timeslot);
            foreach (var item in result)
            {
                timeSlots.Add(new SelectListItem() { Text = item, Value =item  });
            }

            return timeSlots;
        }
        
    }
}