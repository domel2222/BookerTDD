using Booker.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Booker.Tests.Validation
{
    public class DateWithoutTimeAttributeTests
    {
        [Theory]
        [InlineData(true, 0, 0, 0)]
        [InlineData(false, 1, 0, 0)]
        [InlineData(false, 0, 1, 0)]
        [InlineData(false, 0, 0, 1)]
        public void ShouldReturnDateMustBeInTheFuture(bool expectedIsValid, int hour, int minutes, int second)
        {
            var dateTime = new DateTime(2021, 6, 28, hour, minutes, second);

            var attribute = new DateWithoutTimeAttribute();

            var isValid = attribute.IsValid(dateTime);

            isValid.ShouldBe(expectedIsValid);
        }
    }
}
