using Booker.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;

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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder )
        {
            // IConfigurationRoot configuration = new ConfigurationBuilder()
            //.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //.AddJsonFile("appsettings.json")
            //.Build();

            // optionsBuilder.UseSqlServer(configuration.GetConnectionString("EventBase"));

                optionsBuilder.UseSqlServer(
                "Server=LAPTOP-R3P7DCQ0\\SQLEXPRESS;Initial Catalog=EventBase;User ID=Dominik;Password=Dominik6002");

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
