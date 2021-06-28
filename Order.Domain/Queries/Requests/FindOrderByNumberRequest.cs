using System.Collections.Generic;
using System.Linq;

namespace Order.Domain.Queries.Requests
{
    public class FindOrderByNumberRequest
    {
        public string Number { get; private set; }

        private readonly List<string> _errors;
        public IReadOnlyCollection<string> Errors => _errors.ToList();

        public FindOrderByNumberRequest(string number)
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
