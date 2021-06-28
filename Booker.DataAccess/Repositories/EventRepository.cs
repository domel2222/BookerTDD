using Booker.DataInterfaces;
using Booker.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.DataAccess.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventBookerContext _context;

        public EventRepository(EventBookerContext context)
        {
            this._context = context;
        }




        // to refactor this method
        public IEnumerable<Event> GetAvailableEvent(DateTime dateBook)
        {

            // Today Events so you can't book it
            var bookedEventIds = _context.EventBookings
                .Where(x => x.DateTime == dateBook)
                .Select(x => x.EventId);
                


            return _context.Events.Where(x => !bookedEventIds.Contains(x.Id));
        }
    }
}
