using Booker.Web.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Booker.Web.Tests
{
    public class BookEventConfirmationModelTests
    {
        [Fact]
        public void StoreParameretValuesInProperties()
        {
            //arrange
            const int eventBookingid = 13;
            const string firstName = "Marek";
            var date = new DateTime(2021, 8, 1);

            var bookEventConfirmationModel = new BookEventConfirmationModel();

            //act
            bookEventConfirmationModel.OnGet(eventBookingid, firstName, date);

            //assert

            eventBookingid.ShouldBe(bookEventConfirmationModel.EventBookingId);
            firstName.ShouldBe(bookEventConfirmationModel.FirstName);
            date.ShouldBe(bookEventConfirmationModel.Date);
        }
    }
}
