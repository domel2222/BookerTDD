using Booker.DataInterfaces;
using Booker.Modals;
using System;

namespace Booker.Processor
{
    public class EventBookingRequestProcessor
    {
        private readonly IEventBookingRepository _bookingRepository;

        public EventBookingRequestProcessor(IEventBookingRepository bookingRepository, IEventRepository _eventRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public EventBookingResult BookEvent(EventBookingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _bookingRepository.Save(Create<EventBooking>(request));

            return Create<EventBookingResult>(request);
        }

        private  T Create<T>(EventBookingRequest request) where T : BaseEventBooking, new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DateTime = request.DateTime,
            };
        }
    }
}
