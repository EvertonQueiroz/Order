using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Domain.Commands.Requests
{
    public class CreateOrUpdateOrderRequest
    {
        public string Number { get; private set; }

        private readonly IList<OrderItemCommand> _items;
        public IReadOnlyCollection<OrderItemCommand> Items => _items.ToList();

        private readonly List<string> _errors;
        public IReadOnlyCollection<string> Errors => _errors.ToList();

        public CreateOrUpdateOrderRequest(string number)
        {
            Number = number;

            _items = new List<OrderItemCommand>();
            _errors = new List<string>();
        }

        public void AddItem(string description, decimal unitPrice, decimal amount)
        {
            _items.Add(new OrderItemCommand(description, unitPrice, amount));
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Number))
                _errors.Add("Número do pedido é requerido.");

            foreach (var item in _items)
            {
                if (!item.IsValid())
                    _errors.AddRange(item.Errors);
            }

            return !_errors.Any();
        }
    }

    public class OrderItemCommand
    {
        public string Description { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Amount { get; private set; }

        private readonly IList<string> _errors;
        public IReadOnlyCollection<string> Errors => _errors.ToList();

        public OrderItemCommand(string description, decimal unitPrice, decimal amount)
        {
            Description = description;
            UnitPrice = unitPrice;
            Amount = amount;

            _errors = new List<string>();
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Description))
                _errors.Add("O nome do item é requerido.");

            if (UnitPrice <= 0)
                _errors.Add("O valor do item deve ser maior ou igual a zero.");

            if (Amount < 0)
                _errors.Add("A quantidade do item deve ser maior que zero.");

            return !_errors.Any();
        }
    }
}
