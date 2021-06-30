using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booker.DataInterfaces;
using Booker.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booker.Web.Pages
{
    public class EventBookingsModel : PageModel
    {
        private readonly IEventBookingRepository _eventBookingRepository;

        public EventBookingsModel(IEventBookingRepository eventBookingRepository)
        {
            this._eventBookingRepository = eventBookingRepository;
        }

        public IEnumerable<EventBooking> EventBooking { get; set; }

        public void OnGet()
        {
            EventBooking = _eventBookingRepository.GetAll();
        }
    }
}
