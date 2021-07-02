using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Booker.Processor;
using Moq;
using Booker.Web.Pages;
using Booker.Modals;
using NSubstitute;
using Booker.Enums;

namespace Booker.Web.Tests
{
    public class BookEventModelTests
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void CallBookEventMethodOfProcesorIfModelValid_Mock(int expectedBookEvent, bool isModelValid)
        {
            //arrange 

            var processorMock = new Mock<IEventBookingRequestProcessor>();

            var bookEventModel = new BookEventModel(processorMock.Object)
            {
                EventBookingRequest = new EventBookingRequest()
            };
            if (!isModelValid)
            {
                bookEventModel.ModelState.AddModelError("KeyError", "Something wrong");
            }

            //act
            bookEventModel.OnPost();
            //assert
            //processorMock.Verify(x => x.BookEvent(bookEventModel.EventBookingRequest), Times.Once);
            processorMock.Verify(x => x.BookEvent(bookEventModel.EventBookingRequest), Times.Exactly(expectedBookEvent));
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

            var errorValue = "No desk available for selected date";
            var errorName = "EventBookingRequest.DateTime";

            var processorMock = new Mock<IEventBookingRequestProcessor>();

            var bookEventModel = new BookEventModel(processorMock.Object)
            {
                EventBookingRequest = new EventBookingRequest()
            };

            processorMock.Setup(x => x.BookEvent(bookEventModel.EventBookingRequest))
                .Returns(new EventBookingResult
                {
                    Code = EventBookingResultCode.NoEventAvailable
                });

            //act
            bookEventModel.OnPost();

            //assert
            var modelStateEntry = Assert.Contains(errorName, bookEventModel.ModelState);

            //var modelError = Assert.Single(modelStateEntry.Errors);

            var modelError = (modelStateEntry.Errors).First();

            //var modelError = ShouldBe(modelStateEntry.Errors);
            //Assert.Equal("No desk available for selected date", modelError.ErrorMessage);

            errorValue.ShouldBe(modelError.ErrorMessage);
        }

        [Fact]
        public void NotAddModelErrorIfEventIsAvailable_Mock()
        {
            var errorName = "EventBookingRequest.DateTime";

            var processorMock = new Mock<IEventBookingRequestProcessor>();

            var bookEventModel = new BookEventModel(processorMock.Object)
            {
                EventBookingRequest = new EventBookingRequest()
            };

            processorMock.Setup(x => x.BookEvent(bookEventModel.EventBookingRequest))
                .Returns(new EventBookingResult
                {
                    Code = EventBookingResultCode.Success
                });
            //act
            bookEventModel.OnPost();

            //assert

            Assert.DoesNotContain(errorName, bookEventModel.ModelState);
        }

    }
}
