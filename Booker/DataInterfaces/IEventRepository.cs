using Booker.Modals;
using System;
using System.Collections.Generic;

namespace Booker.DataInterfaces
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAvailableEvent(DateTime dateTime);

        IEnumerable<Event> GetAll();
    }

}
