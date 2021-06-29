using Booker.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Booker.Tests.Validation
{
    public class DateInFutureAttributeTests
    {

        [Theory]
        [InlineData(false, -1)]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        public void ValidateDateIsInTheFuture(bool expectedIsValid, int secondsToAdd)
        {
            var dateTimeNow = new DateTime(2021, 6, 28);

            var attribute = new DateInFutureAttribute(() => dateTimeNow);

            var isValid = attribute.IsValid(dateTimeNow.AddSeconds(secondsToAdd));

            expectedIsValid.ShouldBe(isValid);
        }

        [Fact]
        public void ShouldHaveExpectedError()
        {
            var attribute = new DateInFutureAttribute();

            attribute.ErrorMessage.ShouldBe("Date must be in the future");
        }
    }
}
