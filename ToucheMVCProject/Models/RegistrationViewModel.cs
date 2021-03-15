using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToucheMVCProject.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [RegularExpression("[A-Za-z0-9]+",ErrorMessage ="Please enter only alpha-numeric for user Id")]
        [Display(Name ="User Name")]
        [Remote("istaken","LogIn", ErrorMessage = "This user name is already taken")]
        public string userId { get; set; }
        [Required]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Please enter only alphabets for  Name")]
        [Display(Name = "Name")]
        public string username { get; set; }
        [Required]
        [RegularExpression("[A-Za-z0-9 ]+")]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Please enter only alphabets for role")]
        [Display(Name = "Role")]
        public string role { get; set; }
        [Required]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Please enter only numbers for Phone-No")]
        [Display(Name = "Phone-No")]
        public string phoneNo { get; set; }
        [Required]
        [RegularExpression("[A-Za-z0-9 ]+", ErrorMessage = "Please enter only alpha-numeric for Address")]
        [Display(Name = "Address")]
        public string address { get; set; }
    }
}