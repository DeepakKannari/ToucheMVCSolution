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
    
    public partial class reservation
    {
        public int Id { get; set; }
        public string customerId { get; set; }
        public Nullable<int> restaurantId { get; set; }
        public Nullable<int> noOfPeople { get; set; }
        public string timeslot { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual restaurant restaurant { get; set; }
    }
}
