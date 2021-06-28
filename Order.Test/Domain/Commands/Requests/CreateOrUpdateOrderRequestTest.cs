using Order.Domain.Commands.Requests;
using Xunit;

namespace Order.Test.Domain.Commands.Requests
{
    public class CreateOrUpdateOrderRequestTest
    {
        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeValid_WhenAValidOrder()
        {
            var request = new CreateOrUpdateOrderRequest("123456");
            request.AddItem("Escova", 1, 1);

            var isValid = request.IsValid();

            Assert.True(isValid);
            Assert.Empty(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenNumberIsNull()
        {
            var request = new CreateOrUpdateOrderRequest(null);
            request.AddItem("Escova", 1, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenNumberIsEmpty()
        {
            var request = new CreateOrUpdateOrderRequest(string.Empty);
            request.AddItem("Escova", 1, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenThereIsNoItem()
        {
            var request = new CreateOrUpdateOrderRequest("123456");

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenItemDescriptionIsNull()
        {
            var request = new CreateOrUpdateOrderRequest("123456");
            request.AddItem(null, 1, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenItemDescriptionIsEmpty()
        {
            var request = new CreateOrUpdateOrderRequest("123456");
            request.AddItem(string.Empty, 1, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenUnitPriceIsNegative()
        {
            var request = new CreateOrUpdateOrderRequest("123456");
            request.AddItem("Escova", -1, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenAmountIsLowerThanOne()
        {
            var request = new CreateOrUpdateOrderRequest("123456");
            request.AddItem("Escova", 1, 0);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }
    }
}
