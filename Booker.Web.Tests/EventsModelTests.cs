using Booker.Modals;
using System;
using Xunit;
using Moq;
using NSubstitute;
using Booker.DataInterfaces;
using Booker.Web.Pages;
using Shouldly;

namespace Booker.Web.Tests
{
    public class EventsModelTests
    {
        [Fact]
        public void GetAllEventsMock()
        {
            //arrange
            var events = new[]
            {
                new Event(),
                new Event(),
                new Event()
            };

            var eventRepositoryMock = new Mock<IEventRepository>();

            eventRepositoryMock.Setup(x => x.GetAll()).Returns(events);

            var eventsModel = new EventsModel(eventRepositoryMock.Object);

            //act
            eventsModel.OnGet();

            //arrange
            events.ShouldBe(eventsModel.Events);
        }
    }
}
