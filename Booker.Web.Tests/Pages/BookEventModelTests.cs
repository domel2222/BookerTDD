using Booker.Enums;
using Booker.Modals;
using Booker.Processor;
using Booker.Web.Pages;
using Moq;
using NSubstitute;
using Shouldly;
using System.Linq;
using Xunit;

namespace Booker.Web.Tests
{
    public class BookEventModelTests
    {
        private readonly string  _errorValue = "No desk available for selected date";
        private readonly string _errorName = "EventBookingRequest.DateTime";
        private Mock<IEventBookingRequestProcessor> _processorMock;
        private readonly BookEventModel _bookEventModel;
        private readonly EventBookingResult _bookingResult;

        public BookEventModelTests()
        {
            _bookingResult = new EventBookingResult
            {
                Code = EventBookingResultCode.Success
            };

            //mock
            _processorMock = new Mock<IEventBookingRequestProcessor>();

            _bookEventModel = new BookEventModel(_processorMock.Object)
            {
                EventBookingRequest = new EventBookingRequest()
            };
            _processorMock.Setup(x => x.BookEvent(_bookEventModel.EventBookingRequest))
                .Returns(_bookingResult);

            //nsubstitude
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void CallBookEventMethodOfProcesorIfModelValid_Mock(int expectedBookEvent, bool isModelValid)
        {
            //arrange 
            if (!isModelValid)
            {
                _bookEventModel.ModelState.AddModelError("KeyError", "Something wrong");
            }

            //act
            _bookEventModel.OnPost();

            //assert
            //processorMock.Verify(x => x.BookEvent(bookEventModel.EventBookingRequest), Times.Once);
            _processorMock.Verify(x => x.BookEvent(_bookEventModel.EventBookingRequest), Times.Exactly(expectedBookEvent));
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void CallBookEventMethodOfProcesorIfModelValid_NSubstitute(int expectedBookEvent, bool isModelValid)
        {
            //arrange
            var procesorNSub = Substitute.For<IEventBookingRequestProcessor>();

            var bookEventModel = new BookEventModel(procesorNSub)
            {
                EventBookingRequest = new EventBookingRequest()
            };

            procesorNSub.BookEvent(bookEventModel.EventBookingRequest)
                .Returns(new EventBookingResult
                {
                    Code = EventBookingResultCode.Success
                });

            if (!isModelValid)
            {
                bookEventModel.ModelState.AddModelError("KeyError", "Something wrong");
            }

            //act

            bookEventModel.OnPost();

            //assert
            //procesorNSub.Received().BookEvent(bookEventModel.EventBookingRequest);
            procesorNSub.Received(expectedBookEvent).BookEvent(bookEventModel.EventBookingRequest);

        }

        [Fact]
        public void AddModelErrorIfNoEventIsAvailable_Mock()
        {

            //arrange 
            _bookingResult.Code = EventBookingResultCode.NoEventAvailable;

            //act
            _bookEventModel.OnPost();

            //assert
            var modelStateEntry = Assert.Contains(_errorName, _bookEventModel.ModelState);

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
            _bookEventModel.OnPost();

            //assert
            Assert.DoesNotContain(_errorName, _bookEventModel.ModelState);
        }

        [Fact(Skip = "Time will come")]
        public void AddModelErrorIfNoEventIsAvailable_NSubstitude()
        {

        }
        [Fact(Skip = "Time will come")]
        public void NotAddModelErrorIfEventIsAvailable_NSubstitude()
        {

        }
    }
}
