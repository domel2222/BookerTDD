using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booker.Web.Pages
{
    public class BookEventConfirmationModel : PageModel
    {
        public DateTime Date { get; set; }
        public string FirstName { get; set; }
        public int EventBookingId { get; set; }

        public void OnGet(int eventBookingid, string firstName, DateTime date)
        {
            EventBookingId = eventBookingid;
            FirstName = firstName;
            Date = date;
        }
    }
}
