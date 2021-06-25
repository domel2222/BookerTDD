using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Booker.Tests.Processor
{
    public class BookingRequestProcessorTests
    {
        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            var request = new BookingRequest
            {
                FirstName = "Marco",
                LatsName = "Polo",
                Email = "marco@nanan.com",
                DateTime = new DateTime(2021, 5, 25),
            };


            var processor = new BookingRequestProcessor();

            BookingResult result =  processor.BookEvent(request);
        }
        

    }
}
