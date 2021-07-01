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

namespace Booker.Web.Tests
{
    public class BookEventModelTests
    {
        [Fact]
        public void CallBookEventMethodOfProcesor_Mock()
        {
            //arrange 

            var processorMock = new Mock<IEventBookingRequestProcessor>();

            var bookEventModel = new BookEventModel(processorMock.Object)
            {
                EventBookingRequest = new EventBookingRequest()
            };

            //act
            bookEventModel.OnPost();
            //assert
            processorMock.Verify(x => x.BookEvent(bookEventModel.EventBookingRequest), Times.Once);
        }
        [Fact]

        public void CallBookEventMethodOfProcesor_NSubstitute()
        {
            //arrange
            var procesorNSub = Substitute.For<IEventBookingRequestProcessor>();

            var bookEventModel = new BookEventModel(procesorNSub)
            {
                EventBookingRequest = new EventBookingRequest()
            };
            //act

            bookEventModel.OnPost();
            //assert
            procesorNSub.Received().BookEvent(bookEventModel.EventBookingRequest);

        }
    }
}
