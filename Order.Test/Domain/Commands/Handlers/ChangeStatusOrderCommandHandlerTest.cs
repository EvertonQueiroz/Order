using Moq;
using Order.Domain.Commands.Handlers;
using Order.Domain.Commands.Requests;
using Order.Domain.Interfaces.Commands.Handlers;
using Order.Domain.Interfaces.Data.Repositories;
using System.Linq;
using Xunit;

namespace Order.Test.Domain.Commands.Handlers
{
    public class ChangeStatusOrderCommandHandlerTest
    {
        private Mock<IOrderRepository> _orderRepository = new Mock<IOrderRepository>();

        private IChangeStatusOrderCommandHandler _handler;

        private Order.Domain.Order order;

        public ChangeStatusOrderCommandHandlerTest()
        {
            _handler = new ChangeStatusOrderCommandHandler(
                _orderRepository.Object);

            order = new Order.Domain.Order("123456");
            order.AddOrUpdateItem("Item A", 10, 1);
            order.AddOrUpdateItem("Item B", 5, 2);
        }

        [Fact]
        public void ChangeStatus_ShouldReturnApprovedStatus_WhenApprovalOfTheEntireOrder()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(order);

            var request = new ChangeStatusOrderRequest("123456", "APROVADO", 3, 20);

            var response = _handler.Handle(request);
            var responseStatus = response.Status.FirstOrDefault();

            Assert.Equal("123456", response.Number);
            Assert.Single(response.Status);
            Assert.Equal("APROVADO", responseStatus);
        }

        [Fact]
        public void ChangeStatus_ShouldReturnApprovedWithLowerValueStatus_WhenTheApprovedValueIsLessThanTheTotalValue()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(order);

            var request = new ChangeStatusOrderRequest("123456", "APROVADO", 3, 10);

            var response = _handler.Handle(request);
            var responseStatus = response.Status.FirstOrDefault();

            Assert.Equal("123456", response.Number);
            Assert.Single(response.Status);
            Assert.Equal("APROVADO_VALOR_A_MENOR", responseStatus);
        }

        [Fact]
        public void ChangeStatus_ShouldReturnApprovedWithHigherValueStatusAndApprovedWithGreaterAmountStatus_WhenTheApprovedAmountAndAmountAreGreaterThanTheOrder()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(order);

            var request = new ChangeStatusOrderRequest("123456", "APROVADO", 4, 21);

            var response = _handler.Handle(request);

            Assert.Equal("123456", response.Number);
            Assert.Equal(2, response.Status.Count());
            Assert.Contains(response.Status, status => status == "APROVADO_VALOR_A_MAIOR");
            Assert.Contains(response.Status, status => status == "APROVADO_QTD_A_MAIOR");
        }

        [Fact]
        public void ChangeStatus_Second()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(order);

            var request = new ChangeStatusOrderRequest("123456", "APROVADO", 2, 20);

            var response = _handler.Handle(request);

            Assert.Equal("123456", response.Number);
            Assert.Single(response.Status);
            Assert.Contains(response.Status, status => status == "APROVADO_QTD_A_MENOR");
        }

        [Fact]
        public void ChangeStatus_ShouldReturnDisapprovedStatus_WhenDisapprovalIsRequested()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(order);

            var request = new ChangeStatusOrderRequest("123456", "REPROVADO", 0, 0);

            var response = _handler.Handle(request);

            Assert.Equal("123456", response.Number);
            Assert.Single(response.Status);
            Assert.Contains(response.Status, status => status == "REPROVADO");
        }

        [Fact]
        public void ChangeStatus_ShouldReturnOrderNotFoundStatus_WhenAnInvalidOrderNumberIsEntered()
        {
            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns((Order.Domain.Order) null);

            var request = new ChangeStatusOrderRequest("123456-N", "REPROVADO", 0, 0);

            var response = _handler.Handle(request);

            Assert.Equal("123456-N", response.Number);
            Assert.Single(response.Status);
            Assert.Contains(response.Status, status => status == "CODIGO_PEDIDO_INVALIDO");
        }
    }
}
