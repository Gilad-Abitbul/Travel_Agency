using Project___Intro_To_Computer_Networking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project___Intro_To_Computer_Networking.Dal
{
    public class clsCreditCardsDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<cls_creditCard>().ToTable("tblCreditCards");
        }
        public DbSet<cls_creditCard> cards { get; set; }
    }
}