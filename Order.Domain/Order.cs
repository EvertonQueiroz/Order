using System.Collections.Generic;
using System.Linq;

namespace Order.Domain
{
    public class Order
    {
        public string Number { get; set; }

        private readonly IList<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items => _items.ToList();

        protected Order() { }
        public Order(string number)
        {
            Number = number;
            _items = new List<OrderItem>();
        }

        public void AddOrUpdateItem(string description, decimal unitPrice, decimal amount)
        {
            var orderItem = _items.FirstOrDefault(orderItem => orderItem.Description == description);

            if (orderItem is null)
            {
                _items.Add(new OrderItem(description, unitPrice, amount));
                return;
            }

            orderItem.UnitPrice = unitPrice;
            orderItem.Amount = amount;
        }

        public void RemoveItem(OrderItem itemToRemove)
        {
            _items.Remove(itemToRemove);
        }
    }
}
