using Booker.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using NSubstitute;
using Shouldly;
using Booker.DataInterfaces;
using Booker.Web.Pages;

namespace Booker.Web.Tests
{
    public class EventBookingModelTests
    {

        private readonly EventBooking[] _eventBooking = new EventBooking[] 
        {
                new EventBooking(),
                new EventBooking(),
                new EventBooking()
        };

        [Fact]
        public void GetAllEventBooking_Mock()
        {
            //arrange

            var eventBookingRepositoryMock = new Mock<IEventBookingRepository>();

            eventBookingRepositoryMock.Setup(x => x.GetAll()).Returns(_eventBooking);

            var eventBookingModel = new EventBookingsModel(eventBookingRepositoryMock.Object);

            //act

            eventBookingModel.OnGet();

            //assert

            _eventBooking.ShouldBe(eventBookingModel.EventBooking);
        }
        [Fact]
        public void GetAllEventsBooking_NSubstitute()
        {
            //arrange
            var eventBookingRepositoryNSub = Substitute.For<IEventBookingRepository>();

            eventBookingRepositoryNSub.GetAll().Returns(_eventBooking);

            var eventBookingModel = new EventBookingsModel(eventBookingRepositoryNSub);

            //act

            eventBookingModel.OnGet();

            //assert

            _eventBooking.ShouldBe(eventBookingModel.EventBooking);
        }
    }
}
