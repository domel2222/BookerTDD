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
using System.Diagnostics.CodeAnalysis;

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

        [Fact]
        public void GetAll_BookingEventsByDate()
        {
            //arrange
            var eventsLists = new List<EventBooking> {
                CreateEventBooking(1, new DateTime(2021, 7, 21), 1),
                CreateEventBooking(2, new DateTime(2021, 7, 28), 1),
                CreateEventBooking(3, new DateTime(2021, 8, 8), 1)
            };

            
            eventsLists.OrderBy(x => x.DateTime);

            using ( var context = DbContextFactory.CreateDb(nameof(GetAll_BookingEventsByDate)))
            {
                foreach (var item in eventsLists)
                {
                    context.Add(item);
                    context.SaveChanges();
                }
            }
            //act
            List<EventBooking> actualList;
            using (var context = DbContextFactory.CreateDb(nameof(GetAll_BookingEventsByDate)))
            {
                var repository = new EventBookingRepository(context);
                actualList = repository.GetAll().ToList();                
            }
            //assert
            //actualList.ShouldBe(eventsLists);
            actualList.ShouldBe(eventsLists, new EventBookingComparer(), false);
        }



        private class EventBookingComparer : IEqualityComparer<EventBooking>
        {
            public bool Equals([AllowNull] EventBooking x,[AllowNull] EventBooking y)
            {
                return (x.Id == y.Id);
            }

            public int GetHashCode([DisallowNull] EventBooking obj)
            {
                return obj.Id.GetHashCode();
            }
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
