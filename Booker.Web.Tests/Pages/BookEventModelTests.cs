using Booker.Enums;
using Booker.Modals;
using Booker.Processor;
using Booker.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Booker.Web.Tests
{
    public class BookEventModelTests
    {
        private readonly string  _errorValue = "No desk available for selected date";
        private readonly string _errorName = "EventBookingRequest.DateTime";
        private readonly Mock<IEventBookingRequestProcessor> _processorMock;
        private readonly IEventBookingRequestProcessor _procesorNSub;
        private readonly BookEventModel _bookEventModelNSub;
        private readonly BookEventModel _bookEventModelMock;
        private readonly EventBookingResult _bookingResult;

        public BookEventModelTests()
        {
            _bookingResult = new EventBookingResult
            {
                Code = EventBookingResultCode.Success
            };

            //mock
            _processorMock = new Mock<IEventBookingRequestProcessor>();

            _bookEventModelMock = new BookEventModel(_processorMock.Object)
            {
                EventBookingRequest = new EventBookingRequest()
            };
            _processorMock.Setup(x => x.BookEvent(_bookEventModelMock.EventBookingRequest))
                .Returns(_bookingResult);

            //nsubstitude

            _procesorNSub = Substitute.For<IEventBookingRequestProcessor>();

            _bookEventModelNSub = new BookEventModel(_procesorNSub)
            {
                EventBookingRequest = new EventBookingRequest()
            };

            _procesorNSub.BookEvent(_bookEventModelNSub.EventBookingRequest)
                .Returns(_bookingResult);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void CallBookEventMethodOfProcesorIfModelValid_Mock(int expectedBookEvent, bool isModelValid)
        {
            //arrange 
            if (!isModelValid)
            {
                _bookEventModelMock.ModelState.AddModelError("KeyError", "Something wrong");
            }

            //act
            _bookEventModelMock.OnPost();

            //assert
            //processorMock.Verify(x => x.BookEvent(bookEventModel.EventBookingRequest), Times.Once);
            _processorMock.Verify(x => x.BookEvent(_bookEventModelMock.EventBookingRequest), Times.Exactly(expectedBookEvent));
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void CallBookEventMethodOfProcesorIfModelValid_NSubstitute(int expectedBookEvent, bool isModelValid)
        {
            //arrange

            if (!isModelValid)
            {
                _bookEventModelNSub.ModelState.AddModelError("KeyError", "Something wrong");
            }

            //act

            _bookEventModelNSub.OnPost();

            //assert
            //procesorNSub.Received().BookEvent(bookEventModel.EventBookingRequest);
            _procesorNSub.Received(expectedBookEvent).BookEvent(_bookEventModelNSub.EventBookingRequest);

        }

        [Fact]
        public void AddModelErrorIfNoEventIsAvailable_Mock()
        {

            //arrange 
            _bookingResult.Code = EventBookingResultCode.NoEventAvailable;

            //act
            _bookEventModelMock.OnPost();

            //assert
            var modelStateEntry = Assert.Contains(_errorName, _bookEventModelMock.ModelState);

            //var modelError = Assert.Single(modelStateEntry.Errors);

            var modelError = (modelStateEntry.Errors).First();

            //var modelError = ShouldBe(modelStateEntry.Errors);
            //Assert.Equal("No desk available for selected date", modelError.ErrorMessage);

            _errorValue.ShouldBe(modelError.ErrorMessage);
        }

        [Fact]
        public void NotAddModelErrorIfEventIsAvailable_Mock()
        {
            //arrange
            _bookingResult.Code = EventBookingResultCode.Success;

            //act
            _bookEventModelMock.OnPost();

            //assert
            Assert.DoesNotContain(_errorName, _bookEventModelMock.ModelState);
        }

        //[Fact(Skip = "Time will come")]
        [Fact]
        public void AddModelErrorIfNoEventIsAvailable_NSubstitude()
        {
            //arrange 
            _bookingResult.Code = EventBookingResultCode.NoEventAvailable;

            //act
            _bookEventModelNSub.OnPost();

            //assert

            var modelStateEntry = Assert.Contains(_errorName, _bookEventModelNSub.ModelState);

            var modelError = modelStateEntry.Errors.First();

            _errorValue.ShouldBe(modelError.ErrorMessage);
        }

        [Fact]
        public void NotAddModelErrorIfEventIsAvailable_NSubstitude()
        {
            //arrange
            _bookingResult.Code = EventBookingResultCode.Success;

            //act
            _bookEventModelNSub.OnPost();

            //assert
            Assert.DoesNotContain(_errorValue, _bookEventModelNSub.ModelState);

        }

        [Theory]
        [InlineData(typeof(PageResult), false, null)]
        [InlineData(typeof(PageResult), true, EventBookingResultCode.NoEventAvailable)]
        [InlineData(typeof(RedirectToPageResult), true, EventBookingResultCode.Success)]
        public void ReturnExpectedActionResult_NSubstitude(Type expectedActionType, 
                                                bool isModelValid, 
                                                EventBookingResultCode? eventBookingResultCode)
        {
            //arrange
            if (!isModelValid)
            {
                _bookEventModelNSub.ModelState.AddModelError("KeyError", "Something wrong");
            }

            if (eventBookingResultCode.HasValue)
            {
                _bookingResult.Code = eventBookingResultCode.Value;
            }
            //act
            IActionResult actionResult =  _bookEventModelNSub.OnPost();

            //assert
            actionResult.ShouldBeOfType(expectedActionType);
        }

        [Fact]
        public void RedirectToBookEventConfirmationPage_NSubstitude()
        {
            // arrange 
            _bookingResult.Code = EventBookingResultCode.Success;
            _bookingResult.EventBookingId = 13;
            _bookingResult.FirstName = "Marco";
            _bookingResult.DateTime = new DateTime(2022, 2, 12);

            //act
            IActionResult actionResult = _bookEventModelNSub.OnPost();


            //assert

            var redirectToPageResult = actionResult.ShouldBeOfType<RedirectToPageResult>();

            redirectToPageResult.PageName.ShouldBe("BookEventConfirmation");

            IDictionary<string, object> routeValues = redirectToPageResult.RouteValues;

            routeValues.Count().ShouldBe(3);

            var eventBookingId = Assert.Contains("EventBookingId", routeValues);
            _bookingResult.EventBookingId.ShouldBe(eventBookingId);

            var firstName = Assert.Contains("FirstName", routeValues);
            _bookingResult.FirstName.ShouldBe(firstName);

            var dateTime = Assert.Contains("DateTime", routeValues);
            _bookingResult.DateTime.ShouldBe(dateTime);
        }
    }
}
