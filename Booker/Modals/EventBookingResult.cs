using Booker.Enums;
using System;

namespace Booker.Modals
{
    public class EventBookingResult : BaseEventBooking
    {
        public EventBookingResultCode Code { get; set; }
        public int? EventBookingId { get; set; }
    }
}