﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class toucheEntities : DbContext
    {
        public toucheEntities()
            : base("name=toucheEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<address> addresses { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<registration> registrations { get; set; }
        public virtual DbSet<reservation> reservations { get; set; }
        public virtual DbSet<reservationInfo> reservationInfoes { get; set; }
        public virtual DbSet<restaurant> restaurants { get; set; }

        public System.Data.Entity.DbSet<ToucheMVCProject.Models.restaurantViewModel> restaurantViewModels { get; set; }

        public System.Data.Entity.DbSet<ToucheMVCProject.Models.reservationViewModel> reservationViewModels { get; set; }
    }
}
