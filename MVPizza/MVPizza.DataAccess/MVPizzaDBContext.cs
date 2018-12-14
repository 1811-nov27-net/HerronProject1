using System;
using Microsoft.EntityFrameworkCore;

namespace MVPizza.DataAccess
{
    public class MVPizzaDBContext : DbContext
    {
        public MVPizzaDBContext(DbContextOptions<MVPizzaDBContext> options) : base(options)
        { }

        public DbSet<Store> Store { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // using dataannotations for now
        }
    }
}
