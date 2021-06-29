using Booker.Modals;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booker.DataInterfaces
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAvailableEvent(DateTime dateTime);

        //Task<IEnumerable<Event>> GetAll();
        IEnumerable<Event> GetAll();
    }

}
