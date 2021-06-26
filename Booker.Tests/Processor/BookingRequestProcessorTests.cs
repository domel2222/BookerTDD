using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Booker.Modals;
using Booker.Processor;

namespace Booker.Tests.Processor
{
    public class BookingRequestProcessorTests
    {
        private readonly BookingRequestProcessor _processor = new BookingRequestProcessor();
        private readonly BookingRequest _request = new BookingRequest
        {
            FirstName = "Marco",
            LastName = "Polo",
            Email = "marco@nanan.com",
            DateTime = new DateTime(2021, 5, 25),
        };
        

        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {

            //Arrange
            

           
            //act
            BookingResult result =  _processor.BookEvent(_request);

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
            _processor.BookEvent(_request);
        }
    }
}
