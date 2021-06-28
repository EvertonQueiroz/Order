using Order.Domain.Queries.Requests;
using Xunit;

namespace Order.Test.Domain.Queries.Requests
{
    public class FindOrderByNumberRequestTest
    {
        [Fact]
        public void FindOrderByNumberRequest_ShouldBeValid_WhenNumberIsInformed()
        {
            var request = new FindOrderByNumberRequest("123456");

            var isValid = request.IsValid();

            Assert.True(isValid);
            Assert.Empty(request.Errors);
        }

        [Fact]
        public void FindOrderByNumberRequest_ShouldBeInvalid_WhenNumberIsNull()
        {
            var request = new FindOrderByNumberRequest(null);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }

        [Fact]
        public void FindOrderByNumberRequest_ShouldBeInvalid_WhenNumberIsEmpty()
        {
            var request = new FindOrderByNumberRequest(string.Empty);

            var isValid = request.IsValid();

            Assert.True(!isValid);
            Assert.Single(request.Errors);
        }
    }
}
