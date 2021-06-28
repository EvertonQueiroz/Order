using System.Collections.Generic;
using System.Linq;

namespace Order.Domain.Commands.Responses
{
    public class CreateOrUpdateOrderResponse
    {
        public bool IsNew { get; }
        public string Number { get; }
        public IReadOnlyList<CreateOrUpdateOrderItemReponse> Items { get; }

        public CreateOrUpdateOrderResponse(bool isNew, Order order)
        {
            Number = order.Number;
            Items = order.Items.Select(item => new CreateOrUpdateOrderItemReponse(item)).ToList();
        }
    }

    public class CreateOrUpdateOrderItemReponse
    {
        public string Description { get; }
        public decimal UnitPrice { get; }
        public decimal Amount { get; }

        public CreateOrUpdateOrderItemReponse(OrderItem item)
        {
            Description = item.Description;
            UnitPrice = item.UnitPrice;
            Amount = item.Amount;
        }
    }
}
