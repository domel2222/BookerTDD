using Booker.DataInterfaces;
using Booker.Modals;
using System;

namespace Booker.Processor
{
    public class BookingRequestProcessor
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingRequestProcessor(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public BookingResult BookEvent(BookingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _bookingRepository.Save(new Booking
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DateTime = request.DateTime,
            });

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
