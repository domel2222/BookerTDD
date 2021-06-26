using System;
using System.Collections.Generic;
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

namespace Booker.Tests.Processor
{
    public class BookingRequestProcessorTests
    {
        private readonly BookingRequestProcessor _processor;
        private readonly BookingRequestProcessor _processorMock;
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        private IBookingRepository _bookingRepository = Substitute.For<IBookingRepository>();
        private readonly BookingRequest _request = new BookingRequest
        {
            FirstName = "Marco",
            LastName = "Polo",
            Email = "marco@nanan.com",
            DateTime = new DateTime(2021, 5, 25),
        };

        public BookingRequestProcessorTests()
        {
            _processor = new BookingRequestProcessor(_bookingRepository);
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _processorMock = new BookingRequestProcessor(_bookingRepositoryMock.Object);
        }
        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {

            //act
            BookingResult result = _processor.BookEvent(_request);

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
            Booking savedBooking = null;

            //_bookingRepository.When(x=>x.Save(Arg.Any<Booking>())
            //     .Do(Callback.First(
            //     booking =>
            //     {
            //         savedBooking = booking;
            //     });

            _bookingRepository.When(x => x.Save(Arg.Any<Booking>())).Do(book => { savedBooking = (Booking)book[0]; });

            _processor.BookEvent(_request);

            
            _bookingRepository.Received(1).Save(Arg.Any<Booking>());

            savedBooking.ShouldNotBeNull();

            _request.FirstName.ShouldBe(savedBooking.FirstName);
            _request.LastName.ShouldBe(savedBooking.LastName);
            _request.Email.ShouldBe(savedBooking.Email);
            _request.DateTime.ShouldBe(savedBooking.DateTime);
        }


        [Fact]
        public void ShouldSaveEventBookingMoc()
        {
            Booking savedBooking = null;

            _bookingRepositoryMock.Setup(x => x.Save(It.IsAny<Booking>()))
                .Callback<Booking>(booking =>
                {
                    savedBooking = booking;

                });

            _processorMock.BookEvent(_request);

            _bookingRepositoryMock.Verify(x => x.Save(It.IsAny<Booking>()), Times.Once);

            Assert.NotNull(savedBooking);
            Assert.Equal(_request.FirstName, savedBooking.FirstName);
            Assert.Equal(_request.LastName, savedBooking.LastName);
            Assert.Equal(_request.Email, savedBooking.Email);
            Assert.Equal(_request.DateTime, savedBooking.DateTime);
        }
    }
}
