using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booker.Enums;
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
        public IActionResult OnPost()
        {
            IActionResult actionResult = Page();

            if (ModelState.IsValid) 
            { 
                var result = _eventBookingRequestProcessor.BookEvent(EventBookingRequest);
                if (result.Code == EventBookingResultCode.Success)
                {
                    actionResult = RedirectToPage("BookEventConfirmation", new
                    {
                        result.EventBookingId,
                        result.FirstName,
                        result.DateTime
                    });
                }
                else if  (result.Code == EventBookingResultCode.NoEventAvailable)
                {
                    ModelState.AddModelError("EventBookingRequest.DateTime", "No desk available for selected date");
                }
            }
            return actionResult;
        }
    }
}
