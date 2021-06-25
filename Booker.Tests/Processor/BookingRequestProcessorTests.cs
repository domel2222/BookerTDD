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
        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {

            //Arrange
            var request = new BookingRequest
            {
                FirstName = "Marco",
                LastName = "Polo",
                Email = "marco@nanan.com",
                DateTime = new DateTime(2021, 5, 25),
            };

            
            var processor = new BookingRequestProcessor();
            //act
            BookingResult result =  processor.BookEvent(request);

            //assert 

            result.ShouldNotBeNull();

            result.FirstName.ShouldBe(request.FirstName);
            result.LastName.ShouldBe(request.LastName);
            result.Email.ShouldBe(request.Email);
            result.DateTime.ShouldBe(request.DateTime);
        }
        

    }
}
