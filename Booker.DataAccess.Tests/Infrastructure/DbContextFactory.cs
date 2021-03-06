using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.DataAccess.Tests.Infrastructure
{
    public class DbContextFactory
    {
        public static EventBookerContext CreateDb(string name)
        {
            var options = new DbContextOptionsBuilder<EventBookerContext>()
                    .UseInMemoryDatabase(name)
                    .Options;

            return new EventBookerContext(options);
        }
    }
}
