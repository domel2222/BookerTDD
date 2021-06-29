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
    public class EventsModel : PageModel
    {
        private readonly IEventRepository _eventRepository;

        public EventsModel(IEventRepository eventRepository)
        {
            this._eventRepository = eventRepository;
        }

        public IEnumerable<Event> Events { get; set; }

        public void OnGet()
        {
            Events = _eventRepository.GetAll();
        }
    }
}
