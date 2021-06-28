using Moq;
using Order.Domain.Commands.Handlers;
using Order.Domain.Commands.Requests;
using Order.Domain.Exceptions;
using Order.Domain.Interfaces.Commands.Handlers;
using Order.Domain.Interfaces.Data;
using Order.Domain.Interfaces.Data.Repositories;
using System;
using Xunit;

namespace Order.Test.Domain.Commands.Handlers
{
    public class DeleteOrderCommandHandlerTest
    {
        private Mock<IOrderRepository> _orderRepository = new Mock<IOrderRepository>();
        private Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();

        private IDeleteOrderCommandHandler _handler;

        private Order.Domain.Order order;

        public DeleteOrderCommandHandlerTest()
        {
            _handler = new DeleteOrderCommandHandler(
                _orderRepository.Object,
                _unitOfWork.Object);

            order = new Order.Domain.Order("123456");
            order.AddOrUpdateItem("Item A", 10, 1);
            order.AddOrUpdateItem("Item B", 5, 2);
        }

        [Fact]
        public void Delete_ShouldDelete_WhenOrderExists()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(order);

            var request = new DeleteOrderRequest("123456");

            _handler.Handle(request);

            _unitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [Fact]
        public void Delete_ShouldNotDelete_WhenOrderDoesNotExists()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns((Order.Domain.Order)null);

            var request = new DeleteOrderRequest("123456");

            Assert.Throws<OrderNotFoundException>(() => _handler.Handle(request));
        }

        [Fact]
        public void Delete_ShouldNotDelete_WhenTheRequestIsInvalid()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns((Order.Domain.Order)null);

            var request = new DeleteOrderRequest(string.Empty);

            Assert.Throws<InvalidRequestException>(() => _handler.Handle(request));
        }

        [Fact]
        public void Delete_ShouldThrowArgumentNullException_WhenRepositoryIsNotInjected()
        {
            Assert.Throws<ArgumentNullException>(() => new DeleteOrderCommandHandler(null, _unitOfWork.Object));
        }

        [Fact]
        public void Delete_ShouldThrowArgumentNullException_WhenUnitOfWorkIsNotInjected()
        {
            Assert.Throws<ArgumentNullException>(() => new DeleteOrderCommandHandler(_orderRepository.Object, null));
        }
    }
}
