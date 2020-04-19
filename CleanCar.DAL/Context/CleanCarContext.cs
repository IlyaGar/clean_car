using CleanCar.DAL.DBInitializer;
using CleanCar.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace CleanCar.DAL.Context
{
    public class CleanCarContext : DbContext
    {
        public CleanCarContext() : base("clean_db_4")
        {
            Database.SetInitializer(new CleanCarDBInitializer());
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
