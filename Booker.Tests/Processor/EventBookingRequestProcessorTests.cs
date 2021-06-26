using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Booker.Modals;
using Booker.Processor;
using Booker.DataInterfaces;
using NSubstitute;
using Moq;
using System.Collections.Generic;

namespace Booker.Tests.Processor
{
    public class EventBookingRequestProcessorTests
    {
        private readonly EventBookingRequestProcessor _processorMock;
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly Mock<IEventBookingRepository> _bookingRepositoryMock;


        private readonly EventBookingRequestProcessor _processor;
        private IEventBookingRepository _bookingRepository = Substitute.For<IEventBookingRepository>();
        private IEventRepository _eventRepository = Substitute.For<IEventRepository>();
        private IEnumerable<Event> _availableEvent;
        private readonly EventBookingRequest _request = new EventBookingRequest
        {
            FirstName = "Marco",
            LastName = "Polo",
            Email = "marco@nanan.com",
            DateTime = new DateTime(2021, 5, 25),
        };

        public EventBookingRequestProcessorTests()
        {
            _availableEvent = new List<Event> { new Event() };
            //Nsubstitude
            _processor = new EventBookingRequestProcessor(_bookingRepository, _eventRepository);
            _eventRepository.GetAvailableEvent(_request.DateTime).Returns(_availableEvent);

            //Moq
            _bookingRepositoryMock = new Mock<IEventBookingRepository>();
            _eventRepositoryMock = new Mock<IEventRepository>();

            _eventRepositoryMock.Setup(x => x.GetAvailableEvent(_request.DateTime)).Returns(_availableEvent);
            _processorMock = new EventBookingRequestProcessor(_bookingRepositoryMock.Object, _eventRepositoryMock.Object);
        }
        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {

            //act
            EventBookingResult result = _processor.BookEvent(_request);

            //assert 

            result.ShouldNotBeNull();

            result.FirstName.ShouldBe(_request.FirstName);
            result.LastName.ShouldBe(_request.LastName);
            result.Email.ShouldBe(_request.Email);
            result.DateTime.ShouldBe(_request.DateTime);
        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {

            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookEvent(null));


            exception.ParamName.ShouldBe("request");
        }

        [Fact]
        public void ShouldSaveEventBooking()
        {
            EventBooking savedBooking = null;

            //_bookingRepository.When(x=>x.Save(Arg.Any<Booking>())
            //     .Do(Callback.First(
            //     booking =>
            //     {
            //         savedBooking = booking;
            //     });

            _bookingRepository.When(x => x.Save(Arg.Any<EventBooking>())).Do(book => { savedBooking = (EventBooking)book[0]; });

            _processor.BookEvent(_request);

            
            _bookingRepository.Received(1).Save(Arg.Any<EventBooking>());

            savedBooking.ShouldNotBeNull();

            _request.FirstName.ShouldBe(savedBooking.FirstName);
            _request.LastName.ShouldBe(savedBooking.LastName);
            _request.Email.ShouldBe(savedBooking.Email);
            _request.DateTime.ShouldBe(savedBooking.DateTime);
        }


        [Fact]
        public void ShouldSaveEventBookingMoc()
        {
            EventBooking savedBooking = null;

            _bookingRepositoryMock.Setup(x => x.Save(It.IsAny<EventBooking>()))
                .Callback<EventBooking>(booking =>
                {
                    savedBooking = booking;

                });

            _processorMock.BookEvent(_request);

            _bookingRepositoryMock.Verify(x => x.Save(It.IsAny<EventBooking>()), Times.Once);

            Assert.NotNull(savedBooking);
            Assert.Equal(_request.FirstName, savedBooking.FirstName);
            Assert.Equal(_request.LastName, savedBooking.LastName);
            Assert.Equal(_request.Email, savedBooking.Email);
            Assert.Equal(_request.DateTime, savedBooking.DateTime);
        }

        [Fact]
        public void ShouldNotSaveEventBookingIfNoEventIsAvailable()
        {

        }
    }
}
