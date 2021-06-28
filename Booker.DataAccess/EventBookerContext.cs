using Booker.Modals;
using Microsoft.EntityFrameworkCore;
using System;

namespace Booker.DataAccess
{
    public class EventBookerContext : DbContext
    {
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
                
                new Event {Id = 1,  })
        }
    }
}
