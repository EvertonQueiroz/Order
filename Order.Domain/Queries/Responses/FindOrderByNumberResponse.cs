using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Domain.Queries.Responses
{
    public class FindOrderByNumberResponse
    {
        public string Number { get; private set; }
        public IReadOnlyList<OrderItemReponse> Items { get; private set; }

        protected FindOrderByNumberResponse() { }

        public static explicit operator FindOrderByNumberResponse(Order order) =>
            new FindOrderByNumberResponse
            {
                Number = order.Number,
                Items = order.Items.Select(item => (OrderItemReponse)item).ToList()
            };
    }

    public class OrderItemReponse
    {
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public decimal UnitPrice { get; private set; }

        protected OrderItemReponse() { }

        public static explicit operator OrderItemReponse(OrderItem item) =>
            new OrderItemReponse
            {
                Description = item.Description,
                Amount = item.Amount,
                UnitPrice = item.UnitPrice
            };
    }
}
