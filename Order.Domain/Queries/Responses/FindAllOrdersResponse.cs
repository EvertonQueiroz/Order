using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Domain.Queries.Responses
{
    public class FindAllOrdersResponse
    {
        public IReadOnlyList<OrderSummary> Orders { get; private set; }

        public FindAllOrdersResponse(IReadOnlyList<Order> orders)
        {
            Orders = orders.Select(order => new OrderSummary(order)).ToList();
        }
    }

    public class OrderSummary
    {
        public string Number { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal TotalValue { get; private set; }

        public OrderSummary(Order order)
        {
            Number = order.Number;
            TotalAmount = order.Items.Sum(item => item.Amount);
            TotalValue = order.Items.Sum(item => item.Amount * item.UnitPrice);
        }
    }
}
