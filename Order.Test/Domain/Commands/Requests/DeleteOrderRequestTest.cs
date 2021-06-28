using Order.Domain.Commands.Requests;
using Xunit;

namespace Order.Test.Domain.Commands.Requests
{
    public class DeleteOrderRequestTest
    {
        [Fact]
        public void DeleteOrderRequest_ShouldBeValid_WhenNumberIsInformed()
        {
            var request = new DeleteOrderRequest("123456");

            var isValid = request.IsValid();

            Assert.True(isValid);
            Assert.Empty(request.Errors);
        }

        [Fact]
        public void DeleteOrderRequest_ShouldBeInvalid_WhenNumberIsNull()
        {
            var request = new DeleteOrderRequest(null);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void DeleteOrderRequest_ShouldBeInvalid_WhenNumberIsEmpty()
        {
            var request = new DeleteOrderRequest(string.Empty);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }
    }
}
