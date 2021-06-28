using Moq;
using Order.Domain.Interfaces.Data.Repositories;
using Order.Domain.Interfaces.Queries.Handlers;
using Order.Domain.Queries.Handlers;
using Order.Domain.Queries.Requests;
using System;
using System.Collections.Generic;
using Xunit;

namespace Order.Test.Domain.Queries.Handlers
{
    public class FindAllOrdersQueryHandlerTest
    {
        private Mock<IOrderRepository> _orderRepository = new Mock<IOrderRepository>();

        private IFindAllOrdersQueryHandler _handler;

        private List<Order.Domain.Order> _orders;

        public FindAllOrdersQueryHandlerTest()
        {
            _handler = new FindAllOrdersQueryHandler(
                _orderRepository.Object);

            var order = new Order.Domain.Order("123456");
            order.AddOrUpdateItem("Item A", 10, 1);
            order.AddOrUpdateItem("Item B", 5, 2);

            _orders = new List<Order.Domain.Order> { order };
        }

        [Fact]
        public void FindAll_ShouldReturnAllOrders_WhenExistsOrders()
        {
            _orderRepository
                .Setup(m => m.GetAll())
                .Returns(_orders);

            var request = new FindAllOrdersRequest();

            var response = _handler.Handle(request);

            _orderRepository.Verify(m => m.GetAll(), Times.Once);
            Assert.Single(response.Orders);
        }

        [Fact]
        public void FindAll_ShouldReturnEmptyList_WhenDoesNotExistsOrders()
        {
            _orderRepository
                .Setup(m => m.GetAll())
                .Returns(new List<Order.Domain.Order>());

            var request = new FindAllOrdersRequest();

            var response = _handler.Handle(request);

            _orderRepository.Verify(m => m.GetAll(), Times.Once);
            Assert.Empty(response.Orders);
        }

        [Fact]
        public void FindAll_ShouldThrowArgumentNullException_WhenRepositoryIsNotInjected()
        {
            Assert.Throws<ArgumentNullException>(() => new FindAllOrdersQueryHandler(null));
        }
    }
}
