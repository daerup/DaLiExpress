﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DaLiExpress.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DaLi_GameExpressEntities : DbContext
    {
        public DaLi_GameExpressEntities()
            : base("name=DaLi_GameExpressEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DeveloperStudio> DeveloperStudio { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }
    }
}
