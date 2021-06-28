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

        private readonly DateWithoutTimeAttribute _attribute = new DateWithoutTimeAttribute();

        [Theory]
        [InlineData(true, 0, 0, 0)]
        [InlineData(false, 1, 0, 0)]
        [InlineData(false, 0, 1, 0)]
        [InlineData(false, 0, 0, 1)]
        public void ShouldReturnDateMustBeInTheFuture(bool expectedIsValid, int hour, int minutes, int second)
        {
            var dateTime = new DateTime(2021, 6, 28, hour, minutes, second);

            var isValid = _attribute.IsValid(dateTime);

            isValid.ShouldBe(expectedIsValid);
        }
        [Fact]
        public void ShouldHaveExpectedError()
        {
            _attribute.ErrorMessage.ShouldBe("Date must not contain time");
        }
    }
}
