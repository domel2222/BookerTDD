using Booker.Modals;
using Microsoft.EntityFrameworkCore;
using System;

namespace Booker.DataAccess
{
    public class EventBookerContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventBooking> EventBookings { get; set; }

        public EventBookerContext(DbContextOptions<EventBookerContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }


        

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasData(

                new Event { Id = 1, Description = "Catching crabs" },
                new Event { Id = 2, Description = "Hunting for mosquitoes" }
                );
        }
    }
}
