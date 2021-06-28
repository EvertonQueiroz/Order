using System.Collections.Generic;
using System.Linq;

namespace Order.Domain.Commands.Requests
{
    public class DeleteOrderRequest
    {
        public string Number { get; private set; }

        private readonly List<string> _errors;
        public IReadOnlyCollection<string> Errors => _errors.ToList();

        public DeleteOrderRequest(string number)
        {
            Number = number;
            _errors = new List<string>();
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Number))
                _errors.Add("Número do pedido é requerido.");

            return !_errors.Any();
        }
    }
}
