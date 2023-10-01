using Project___Intro_To_Computer_Networking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project___Intro_To_Computer_Networking.Dal
{
    public class clsFlightsDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<cls_flight>().ToTable("tblFlights");
        }
        public DbSet<cls_flight> flights { get; set; }
    }
}