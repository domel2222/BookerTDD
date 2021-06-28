using Booker.DataInterfaces;
using Booker.Enums;
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
            var result = Create<EventBookingResult>(request);

            var availableEvents = _eventRepository.GetAvailableEvent(request.DateTime);
            if (availableEvents.FirstOrDefault() is Event availableEvent)
            {
                availableEvent = availableEvents.First();
                var eventBooking = Create<EventBooking>(request);
                eventBooking.EventId = availableEvent.Id;

                _bookingRepository.Save(eventBooking);

                result.Code = EventBookingResultCode.Success;
            }
            else
            {
                result.Code = EventBookingResultCode.NoEventAvailable;
            }
            return result;
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
