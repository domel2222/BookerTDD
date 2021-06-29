using Booker.DataAccess.Repositories;
using Booker.DataAccess.Tests.Infrastructure;
using Booker.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Booker.DataAccess.Tests.Repositories
{
    public class EventBookingRepositoryTests
    {
        [Fact]
        public void SaveEventBookintInDatabase()
        {
            //arrange

            var eventBooking = CreateEventBooking(1, new DateTime(2021, 8, 8), 1);
            //act
            using (var context = DbContextFactory.CreateDb(nameof(SaveEventBookintInDatabase)))
            {
                var repository = new EventBookingRepository(context);
                repository.Save(eventBooking);
            }

            //assert 
            using (var context = DbContextFactory.CreateDb(nameof(SaveEventBookintInDatabase)))
            {
                var bookings = context.EventBookings.FirstOrDefault();


                bookings.FirstName.ShouldBe(eventBooking.FirstName);
                bookings.LastName.ShouldBe(eventBooking.LastName);
                bookings.Email.ShouldBe(eventBooking.Email);
                bookings.DateTime.ShouldBe(eventBooking.DateTime);
                bookings.EventId.ShouldBe(eventBooking.EventId);
               
            };
        }










        private EventBooking CreateEventBooking(int id, DateTime date, int eventId)
        {
            var eventBooking = new EventBooking
            {
                Id = id,
                FirstName = "Marek",
                LastName = "Popey",
                DateTime = date,
                Email = "marekPopey@.gmail.com",
                EventId = eventId
            };

            return eventBooking;
        }
    }
}
