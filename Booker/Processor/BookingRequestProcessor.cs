using Booker.Modals;
using System;

namespace Booker.Processor
{
    public class BookingRequestProcessor
    {
        public BookingRequestProcessor()
        {
        }

        public BookingResult BookEvent(BookingRequest request)
        {
            return new BookingResult
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DateTime = request.DateTime,
            };
        }
    }
}
