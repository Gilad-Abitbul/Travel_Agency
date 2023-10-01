using Project___Intro_To_Computer_Networking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project___Intro_To_Computer_Networking.Dal
{
    public class clsUsersDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<cls_user>().ToTable("tblUsers");
        }
        public DbSet<cls_user> users { get; set; }
    }
}