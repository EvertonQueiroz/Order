using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Domain.Exceptions
{
    public class InvalidRequestException : Exception
    {
        private readonly List<string> _erros;
        public IReadOnlyCollection<string> Errors => _erros.ToList();

        public InvalidRequestException(IEnumerable<string> errors) : base("Invalid request")
        {
            _erros = new List<string>();
            _erros.AddRange(errors);
        }
    }
}
