using Booker.Modals;

namespace Booker.Processor
{
    public interface IEventBookingRequestProcessor
    {
        EventBookingResult BookEvent(EventBookingRequest request);
    }
}