//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToucheMVCProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu
    {
        public int restaurantId { get; set; }
        public int menuItemId { get; set; }
        public string dishName { get; set; }
        public string description { get; set; }
        public string vtype { get; set; }
        public Nullable<double> price { get; set; }
        public string img { get; set; }
    
        public virtual restaurant restaurant { get; set; }
    }
}
