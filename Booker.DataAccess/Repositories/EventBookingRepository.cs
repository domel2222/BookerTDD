using Booker.DataInterfaces;
using Booker.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.DataAccess.Repositories
{
    public class EventBookingRepository : IEventBookingRepository
    {
        private readonly EventBookerContext _context;

        public EventBookingRepository(EventBookerContext context)
        {
            this._context = context;
        }

        public IEnumerable<EventBooking> GetAll()
        {
            return _context.EventBookings.OrderBy(x => x.DateTime);
        }

        public void Save(EventBooking booking)
        {
            _context.EventBookings.Add(booking);
            _context.SaveChanges();
        }
    }
}
