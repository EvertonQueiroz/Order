using System.Collections.Generic;

namespace Order.Domain.Commands.Responses
{
    public class CreateOrUpdateOrderResponse
    {
        public string Number { get; private set; }
        public List<OrderItemReponse> Items { get; set; }

        public CreateOrUpdateOrderResponse(Order order)
        {
            Number = order.Number;

            Items = new List<OrderItemReponse>();
            foreach (var item in order.Items)
            {
                Items.Add(new OrderItemReponse(item.Description, item.UnitPrice, item.Amount));
            }
        }
    }

    public class OrderItemReponse
    {
        public string Description { get; private set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }

        public OrderItemReponse(string description, decimal unitPrice, decimal amount)
        {
            Description = description;
            UnitPrice = unitPrice;
            Amount = amount;
        }
    }
}
