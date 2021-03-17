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
        [RegularExpression("[0-9]+", ErrorMessage = "Please enter only numbers for reservation Id")]
        [Range(1, 1000)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Customer Id")]
        [RegularExpression("[A-Za-z0-9]+", ErrorMessage = "Please enter only alpha-numeric for user Id")]
        [Range(1, 1000)]
        public string customerId { get; set; }
        [Required]
        [Display(Name = "Restaurant Id")]
        [RegularExpression("[0-9]+", ErrorMessage = "Please enter only numbers for Restaurant Id")]
        [Range(1, 1000)]
        public Nullable<int> restaurantId { get; set; }
        [Required]
        [Display(Name = "Time Slot")]
        public string timeslot { get; set; }
        [Required]
        [Display(Name = "No. of people")]
        [Range(1, 1000)]
        [Remote("isSeatAvailable", "Customer", AdditionalFields = "restaurantId,timeslot", ErrorMessage = "That many seats are not available")]
        public Nullable<int> noOfPeople { get; set; }

        [Required]
        [Display(Name = "Restaurant Name")]
        [RegularExpression("[a-zA-Z ]+", ErrorMessage = "Please enter only alphabet for Restaurant Name")]
        public string restaurantName { get; set; }

        public List<SelectListItem> timeSlots = new List<SelectListItem>();
        //{
        //    new SelectListItem() { Text="9:30-12:30",Value="9:30-12:30"},
        //    new SelectListItem() { Text="12:30-15:30",Value="12:30-15:30"},
        //    new SelectListItem() { Text="15:30-18:30",Value="15:30-18:30"},
        //    new SelectListItem() { Text="18:30-21:30",Value="18:30-21:30"},

        //};
        public List<SelectListItem> populateTimeSlot(int id)
        {
            var result = dbContext.reservationInfoes.Where(s => s.resturantId.Equals(id)).Select(s => s.Timeslot);
            foreach (var item in result)
            {
                timeSlots.Add(new SelectListItem() { Text = item, Value = item });
            }

            return timeSlots;
        }

        public reservation reservationVallues()
        {

            reservation reserve = new reservation();
            reserve.restaurantId = this.restaurantId;
            reserve.timeslot = this.timeslot;

            return reserve;
        }
    }
        
        
    }
