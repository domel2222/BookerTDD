using Booker.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.Tests.Processor.Validation
{
    public class DateInFutureAttributeTests
    {
        public void ShouldReturndatamustBeInTheFuture(bool expectedIsValid, int hour, int minute, int second)
        {
            var dateTime = new DateTime(2021, 6, 28, hour, minute, second);

            var attribute = new DateWithoutTimeAttribute();
        }
    }
}
