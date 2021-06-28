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

        public IEnumerable<Event> GetAvailableEvent(DateTime dateTime)
        {
            
        }
    }
}
