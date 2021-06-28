using Booker.DataAccess.Repositories;
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
    public class EventRepositoryTests
    {
        [Fact]
        public void ShouldReturnTheAvailableEvent()
        {
            var date = new DateTime(2021, 7, 2);

            var options = new DbContextOptionsBuilder<EventBookerContext>()
                    .UseInMemoryDatabase(databaseName: "ShouldReturnTheAvailableEvent")
                    .Options;


            using (var context = new EventBookerContext(options))
            {
                context.Events.Add(new Event { Id = 1 });
                context.Events.Add(new Event { Id = 2 });
                context.Events.Add(new Event { Id = 3 });

                context.EventBookings.Add(new EventBooking { EventId = 1, DateTime = date });
                context.EventBookings.Add(new EventBooking { EventId = 2, DateTime = date.AddDays(13) });


                context.SaveChanges();
            }

            using (var context = new EventBookerContext(options))
            {
                var repository = new EventRepository(context);

                //act
                var eventResult = repository.GetAvailableEvent(date);


                //assert
                eventResult.Count().ShouldBe(2);

            }
        }
    }
}
