using Moq;
using Order.Domain.Exceptions;
using Order.Domain.Interfaces.Data.Repositories;
using Order.Domain.Interfaces.Queries.Handlers;
using Order.Domain.Queries.Handlers;
using Order.Domain.Queries.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Order.Test.Domain.Queries.Handlers
{
    public class FindOrderByNumberQueryHandlerTest
    {
        private Mock<IOrderRepository> _orderRepository = new Mock<IOrderRepository>();

        private IFindOrderByNumberQueryHandler _handler;

        private Order.Domain.Order _order;

        public FindOrderByNumberQueryHandlerTest()
        {
            _handler = new FindOrderByNumberQueryHandler(
                _orderRepository.Object);

            _order = new Order.Domain.Order("123456");
            _order.AddOrUpdateItem("Item A", 10, 1);
            _order.AddOrUpdateItem("Item B", 5, 2);
        }

        [Fact]
        public void FindOrderByNumber_ShouldReturnOrder_WhenExists()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(_order);

            var request = new FindOrderByNumberRequest("123456");

            var response = _handler.Handle(request);

            _orderRepository.Verify(m => m.Get(It.IsAny<string>()), Times.Once);
            Assert.Equal("123456", response.Number);
            Assert.Equal(2, response.Items.Count());
        }

        [Fact]
        public void FindOrderByNumber_ShouldThrownOrderNotFoundException_WhenDoesNotExists()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns((Order.Domain.Order)null);

            var request = new FindOrderByNumberRequest("123456");

            Assert.Throws<OrderNotFoundException>(() => _handler.Handle(request));
        }

        [Fact]
        public void FindOrderByNumber_ShouldThrownInvalidRequestException_WhenNumberDoesNotInformed()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns((Order.Domain.Order)null);

            var request = new FindOrderByNumberRequest(null);

            Assert.Throws<InvalidRequestException>(() => _handler.Handle(request));
        }

        [Fact]
        public void FindOrderByNumber_ShouldThrowArgumentNullException_WhenRepositoryIsNotInjected()
        {
            Assert.Throws<ArgumentNullException>(() => new FindOrderByNumberQueryHandler(null));
        }
    }
}
