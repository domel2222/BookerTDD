using Booker.Modals;
using Microsoft.EntityFrameworkCore;
using System;

namespace Booker.DataAccess
{
    public class EventDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventBooking> EventBookings { get; set; }

        public EventDbContext(DbContextOptions<EventDbContext> options) 
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

                new { Id = 1, Description = "Catching crabs" },
                new { Id = 2, Description = "Hunting for mosquitoes" }
                );
        }
    }
}
