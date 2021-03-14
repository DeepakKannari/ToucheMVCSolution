using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToucheMVCProject.Models
{
    public class RegistrationViewModel
    {
        [Required]
        public string userId { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string role { get; set; }
        [Required]
        public string phoneNo { get; set; }
        [Required]
        public string address { get; set; }
    }
}