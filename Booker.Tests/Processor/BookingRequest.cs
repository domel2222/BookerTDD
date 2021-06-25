using System;

namespace Booker.Tests.Processor
{
    internal class BookingRequest
    {
        public string FirstName { get; set; }
        public string LatsName { get; set; }
        public string Email { get; set; }
        public DateTime DateTime { get; set; }
    }
}