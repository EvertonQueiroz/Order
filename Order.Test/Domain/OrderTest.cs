using System.Linq;
using Xunit;

namespace Order.Test.Domain
{
    public class OrderTest
    {
        [Fact]
        public void Order_ShouldUpdateItem_WhenAlreadyHasItem()
        {
            var order = new Order.Domain.Order("123456");
            order.AddOrUpdateItem("Item A", 10, 1);
            order.AddOrUpdateItem("Item B", 5, 2);
            order.AddOrUpdateItem("Item B", 7, 3);

            Assert.Equal(2, order.Items.Count());

            var itemB = order.Items.FirstOrDefault(item => item.Description == "Item B");

            Assert.Equal(7, itemB?.UnitPrice);
            Assert.Equal(3, itemB?.Amount);
        }

        [Fact]
        public void Order_ShouldRemoveItem_WhenExists()
        {
            var order = new Order.Domain.Order("123456");
            order.AddOrUpdateItem("Item A", 10, 1);
            order.AddOrUpdateItem("Item B", 5, 2);

            var itemB = order.Items.FirstOrDefault(item => item.Description == "Item B");
            order.RemoveItem(itemB);

            Assert.Single(order.Items);

            itemB = order.Items.FirstOrDefault(item => item.Description == "Item B");
            Assert.Null(itemB);
        }
    }
}
