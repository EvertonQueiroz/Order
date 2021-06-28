using System;

namespace Order.Domain.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(string number)
            : base($"Pedido {number} não encontrado.") { }
    }
}
