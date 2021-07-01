using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booker.Modals;
using Booker.Processor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booker.Web.Pages
{
    public class BookEventModel : PageModel
    {
        private IEventBookingRequestProcessor _eventBookingRequestProcessor;

        public BookEventModel(IEventBookingRequestProcessor eventBookingRequestProcessor)
        {
            this._eventBookingRequestProcessor = eventBookingRequestProcessor;
        }

        [BindProperty]
        public EventBookingRequest EventBookingRequest { get; set; }
        public void OnPost()
        {
            _eventBookingRequestProcessor.BookEvent(EventBookingRequest);
        }
    }
}
