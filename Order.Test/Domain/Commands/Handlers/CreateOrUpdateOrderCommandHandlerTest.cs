using Moq;
using Order.Domain.Commands.Handlers;
using Order.Domain.Commands.Requests;
using Order.Domain.Exceptions;
using Order.Domain.Interfaces.Commands.Handlers;
using Order.Domain.Interfaces.Data;
using Order.Domain.Interfaces.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Order.Test.Domain.Commands.Handlers
{
    public class CreateOrUpdateOrderCommandHandlerTest
    {
        private Mock<IOrderRepository> _orderRepository = new Mock<IOrderRepository>();
        private Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();

        private ICreateOrUpdateOrderCommandHandler _handler;

        public CreateOrUpdateOrderCommandHandlerTest()
        {
            _handler = new CreateOrUpdateOrderCommandHandler(
                _orderRepository.Object,
                _unitOfWork.Object);
        }

        [Fact]
        public async Task Order_ShouldBeCreated_WhenIsAValidOrder()
        {
            string number = "1234";
            var itemName = "Escova";

            var request = new CreateOrUpdateOrderRequest(number);
            request.AddItem(itemName, 1, 1);

            var response = _handler.Handle(request);
            var responseItem = response.Items.FirstOrDefault(c => c.Description == itemName);

            Assert.Equal(number, response.Number);
            Assert.Single(response.Items);
            Assert.Equal(1, responseItem?.Amount);
            Assert.Equal(1, responseItem?.UnitPrice);
        }

        [Fact]
        public void Order_ShouldNotBeCreatedOrUpdate_WhenIsANewInvalidOrder()
        {
            var request = new CreateOrUpdateOrderRequest(string.Empty);

            Assert.Throws<InvalidRequestException>(() => _handler.Handle(request));
        }

        [Fact]
        public void OrderItem_ShouldBeChanged_WhenAmountAndUnitPriceAreChanged()
        {
            string number = "1234";
            var itemName = "Escova";

            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns((Order.Domain.Order)null);

            var request = new CreateOrUpdateOrderRequest(number);
            request.AddItem(itemName, 1, 1);

            var response = _handler.Handle(request);
            var responseItem = response.Items.FirstOrDefault(c => c.Description == itemName);

            Assert.Equal(number, response.Number);
            Assert.Single(response.Items);
            Assert.Equal(1, responseItem?.Amount);
            Assert.Equal(1, responseItem?.UnitPrice);
        }

        [Fact]
        public void OrderItem_ShouldBeAdded_WhenANewItemIsAddedToTheOrder()
        {
            var number = "1234";
            var itemName = "Escova";

            var originalOrder = new Order.Domain.Order(number);
            originalOrder.AddOrUpdateItem(itemName, 1, 1);

            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(originalOrder);

            var request = new CreateOrUpdateOrderRequest(number);
            request.AddItem(itemName, 2, 2);

            var response = _handler.Handle(request);
            var responseItem = response.Items.FirstOrDefault(c => c.Description == itemName);

            Assert.Equal(number, response.Number);
            Assert.Single(response.Items);
            Assert.Equal(2, responseItem?.Amount);
            Assert.Equal(2, responseItem?.UnitPrice);
        }

        [Fact]
        public async Task OrderItem_ShouldBeRemoved_WhenAItemIsRemovedFromOriginalOrder()
        {
            var number = "1234";
            var itemName = "Escova";
            var newItemName = "Pente";

            var originalOrder = new Order.Domain.Order(number);
            originalOrder.AddOrUpdateItem(itemName, 1, 1);

            _orderRepository
                .Setup(m => m.Get(It.IsAny<string>()))
                .Returns(originalOrder);

            var request = new CreateOrUpdateOrderRequest(number);
            request.AddItem(itemName, 1, 1);
            request.AddItem(newItemName, 2, 2);

            var response = _handler.Handle(request);
            var responseItem = response.Items.FirstOrDefault(c => c.Description == newItemName);

            Assert.Equal(number, response.Number);
            Assert.Equal(2, response.Items.Count());
            Assert.Equal(2, responseItem?.Amount);
            Assert.Equal(2, responseItem?.UnitPrice);
        }
    }
}
