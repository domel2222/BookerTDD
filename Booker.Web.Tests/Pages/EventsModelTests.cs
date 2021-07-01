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

        private readonly Event[] _events = new[]
            {
                new Event(),
                new Event(),
                new Event()
            };

        [Fact]
        public void GetAllEvents_Mock()
        {
            //arrange
            var eventRepositoryMock = new Mock<IEventRepository>();

            eventRepositoryMock.Setup(x => x.GetAll()).Returns(_events);

            var eventsModel = new EventsModel(eventRepositoryMock.Object);

            //act
            eventsModel.OnGet();

            //arrange
            _events.ShouldBe(eventsModel.Events);
        }

        [Fact]
        public void GetAllEvents_Nsubstitude()
        {
            //arrange
            var eventRepositoryNsub = Substitute.For<IEventRepository>();

            eventRepositoryNsub.GetAll().Returns(_events);

            var eventsModel = new EventsModel(eventRepositoryNsub);

            //act
            eventsModel.OnGet();

            //arrange
            _events.ShouldBe(eventsModel.Events);

        }
    }
}
