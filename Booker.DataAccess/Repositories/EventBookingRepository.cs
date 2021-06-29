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

        public void Save(EventBooking booking)
        {
            throw new NotImplementedException();
        }
    }
}
