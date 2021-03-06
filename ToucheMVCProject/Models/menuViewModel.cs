using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToucheMVCProject.Models
{
    public class menuViewModel
    {
        toucheEntities dbcontext = new toucheEntities();
        [Required]
        [Display(Name ="Restaurant Id")]
        [RegularExpression("[0-9]+",ErrorMessage = "Please enter only positive integers for Restaurant Id")]
        [Range(1,1000)]
        public int restaurantId { get; set; }
        [Required]
        [Display(Name = "MenuItem Id")]
        [RegularExpression("[0-9]+", ErrorMessage = "Please enter only positive integers for MenuItem Id")]
        [Range(1, 1000)]
        public int menuItemId { get; set; }
        [Required]
        [Display(Name = "Dish Name")]
        [RegularExpression("[A-Za-z0-9 ]+", ErrorMessage = "Please enter only alpha-numeric for dish name ")]
        public string dishName { get; set; }
        [Required]
        [Display(Name = "Description")]
        [RegularExpression("[A-Za-z0-9 ]+", ErrorMessage = "Please enter only alpha-numeric for description")]
        public string description { get; set; }
        [Required]
        [Display(Name = "Veg/Non-Veg")]
        public string vtype { get; set; }
        [Required]
        [Display(Name = "Cuisine Type")]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Please enter only alpha-numeric for description")]
        public string cuisinetype { get; set; }
        [Required]
        [Display(Name = "Price")]
        [RegularExpression("[0-9 ]+", ErrorMessage = "Please enter only numeric for Price")]
        [Range(1, 10000)]
        public Nullable<double> price { get; set; }
        [Required]
        [Display(Name = "Image URL")]
        public string img { get; set; }

        public List<SelectListItem> types= new List<SelectListItem>()
        {
            new SelectListItem() { Text="Veg",Value="veg"},
            new SelectListItem() { Text="Non-Veg",Value="non-veg"},
        };

        public List<SelectListItem> restaurantIds = new List<SelectListItem>();
        public void  populateDropdownlists()
        {
            var result = dbcontext.restaurants.Select(s => s.id);
            foreach (var item in result)
            {
                restaurantIds.Add(new SelectListItem() { Text = item+"", Value = item+"" });
            }
        }

        public Menu menuInfoValues()
        {
            Menu menuItem = new Menu();
            menuItem.restaurantId = this.restaurantId;
            menuItem.menuItemId = this.menuItemId;
            menuItem.description = this.description;
            menuItem.vtype = this.vtype;
            //menuItem.cuisinetype = this.cuisinetype;
            menuItem.img = this.img;
            menuItem.dishName = this.dishName;
            menuItem.price = this.price;


            return menuItem;
        }

    }
}