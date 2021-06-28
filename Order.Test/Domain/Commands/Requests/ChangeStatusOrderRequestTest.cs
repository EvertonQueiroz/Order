using Order.Domain.Commands.Requests;
using Xunit;

namespace Order.Test.Domain.Commands.Requests
{
    public class ChangeStatusOrderRequestTest
    {
        [Fact]
        public void ChangeStatusOrderRequest_ShouldBeValid_WhenAValidRequestToApprove()
        {
            var request = new ChangeStatusOrderRequest("123456", "APROVADO", 1, 1);

            var isValid = request.IsValid();

            Assert.True(isValid);
            Assert.Empty(request.Errors);
        }

        [Fact]
        public void ChangeStatusOrderRequest_ShouldBeValid_WhenAValidRequestToDisapprove()
        {
            var request = new ChangeStatusOrderRequest("123456", "REPROVADO", 0, 0);

            var isValid = request.IsValid();

            Assert.True(isValid);
            Assert.Empty(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenNumberIsNull()
        {
            var request = new ChangeStatusOrderRequest(null, "APROVADO", 1, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenNumberIsEmpty()
        {
            var request = new ChangeStatusOrderRequest(string.Empty, "APROVADO", 1, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenStatusIsNull()
        {
            var request = new ChangeStatusOrderRequest("123456", null, 1, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenStatusIsEmpty()
        {
            var request = new ChangeStatusOrderRequest("123456", string.Empty, 1, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }


        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenStatusIsInvalid()
        {
            var request = new ChangeStatusOrderRequest("123456", "TESTAR", 1, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenRequestToApprovalWithApprovedAmountIsLowerOrEqualToZero()
        {
            var request = new ChangeStatusOrderRequest("123456", "APROVADO", 0, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenRequestToApprovalWithApprovedValueIsLowerOrEqualToZero()
        {
            var request = new ChangeStatusOrderRequest("123456", "APROVADO", 1, 0);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenRequestToDisapprovalWithApprovedAmountIsDifferentZero()
        {
            var request = new ChangeStatusOrderRequest("123456", "APROVADO", 0, 1);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void CreateOrUpdateOrderRequest_ShouldBeInvalid_WhenRequestToDisapprovalWithApprovedValueIsDifferentZero()
        {
            var request = new ChangeStatusOrderRequest("123456", "APROVADO", 1, 0);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }
    }
}
