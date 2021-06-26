using Booker.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.DataInterfaces
{
    public interface IEventBookingRepository
    {
        void Save(EventBooking booking);
    }
}
