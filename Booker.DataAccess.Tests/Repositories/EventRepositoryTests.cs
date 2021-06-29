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
        public void ReturnTheAvailableEvent()
        {
            //arrange
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
                eventResult.ShouldContain(x => x.Id == 2);
                eventResult.ShouldContain(x => x.Id == 3);
                eventResult.ShouldNotContain(x => x.Id == 1);

            }

          
        }
        [Fact]
        public void ReturnAllEvents()
        {
            var options = new DbContextOptionsBuilder<EventBookerContext>()
                .UseInMemoryDatabase(databaseName: "ShouldGetAll")
                .Options;

            var patternList = new List<Event>
            {
                new Event(),
                new Event(),
                new Event(),
            };

            using (var context = new EventBookerContext(options))
            {
                foreach (var item in patternList)
                {
                    context.Add(item);
                    context.SaveChanges();
                }
            }
            List<Event> actualList;
            using ( var context = new EventBookerContext(options))
            {
                var repository = new EventRepository(context);
                actualList = repository.GetAll().ToList();
            }

            actualList.Count().ShouldBe(patternList.Count());
        }
    }
}
