using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Domain.Queries.Responses
{
    public class FindOrderByNumberResponse
    {
        public string Number { get; private set; }
        public IReadOnlyList<OrderItemReponse> Items { get; private set; }

        public FindOrderByNumberResponse(Order order) {

            Number = order.Number;
            Items = order.Items.Select(item => new OrderItemReponse(item.Description, item.Amount, item.UnitPrice)).ToList();
        }
    }

    public class OrderItemReponse
    {
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public decimal UnitPrice { get; private set; }


        public OrderItemReponse(string description, decimal amount, decimal unitPrice)
        {
            Description = description;
            Amount = amount;
            UnitPrice = unitPrice;
        }
    }
}
