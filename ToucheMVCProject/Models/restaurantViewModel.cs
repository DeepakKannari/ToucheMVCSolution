using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToucheMVCProject.Models
{
    public class restaurantViewModel
    {
        toucheEntities dbcontext = new toucheEntities();
        [Required]
        [Range(0, 1000, ErrorMessage = "The id should be between 0-1000")]
        
        public int id { get; set; }
        [Required]
        [RegularExpression("[A-Za-z ]+",ErrorMessage = "please only use alphabet")]
        public string name { get; set; }
        [Required]
        [RegularExpression("[A-Za-z0-9 ]+", ErrorMessage = "please only use alphabet and numbers")]
        public string address { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "please only use alphabet")]
        public string category { get; set; }
        [Required]
        public Nullable<System.TimeSpan> opentime { get; set; }
        [Required]
        public Nullable<System.TimeSpan> closetime { get; set; }
        [Required]
        
      
        [RegularExpression("[A-Za-z ]+")]
        public string city { get; set; }

        public string selectedLocation { get; set; }

        [Required]
        public string img { get; set; }

        public List<SelectListItem> listOfStatus = new List<SelectListItem>()
        {
            new SelectListItem() { Text="Open",Value="open"},
            new SelectListItem() { Text="Closed",Value="closed"}
        };

        public List<SelectListItem> restaurantlocations = new List<SelectListItem>();

        public void populateLocation()
        {
            var result = dbcontext.restaurants.Select(s => s.city);
            foreach (var item in result)
            {
                restaurantlocations.Add(new SelectListItem() { Text = item + "", Value = item + "" });
            }
        }
       

        public restaurant getRestaurantValues()
        {
            restaurant restaurantobj = new restaurant();
            restaurantobj.id = this.id;
            restaurantobj.name = this.name;
            restaurantobj.address = this.address;
            restaurantobj.status = this.status;
            restaurantobj.category = this.category;
            //restaurantobj.opentime = this.opentime;
            //restaurantobj.closetime = this.closetime;
            restaurantobj.city = this.city;
            restaurantobj.img = this.img;


            return restaurantobj;
        }

        

    }
}