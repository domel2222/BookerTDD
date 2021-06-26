using Booker.DataInterfaces;
using Booker.Modals;
using System;
using System.Linq;

namespace Booker.Processor
{
    public class EventBookingRequestProcessor
    {
        private readonly IEventBookingRepository _bookingRepository;
        private readonly IEventRepository _eventRepository;

        public EventBookingRequestProcessor(IEventBookingRepository bookingRepository, IEventRepository eventRepository)
        {
            _bookingRepository = bookingRepository;
            this._eventRepository = eventRepository;
        }

        public EventBookingResult BookEvent(EventBookingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var availableEvent = _eventRepository.GetAvailableEvent(request.DateTime);
            if (availableEvent.Count() > 0)
            {
                _bookingRepository.Save(Create<EventBooking>(request));
            }

            return Create<EventBookingResult>(request);
        }

        private T Create<T>(EventBookingRequest request) where T : BaseEventBooking, new()
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
