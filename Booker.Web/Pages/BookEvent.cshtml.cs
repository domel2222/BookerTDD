using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booker.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booker.Web.Pages
{
    public class BookEventModel : PageModel
    {
        [BindProperty]
        public EventBookingRequest EventBookingRequest { get; set; }
        public void OnPost()
        {
        }
    }
}
