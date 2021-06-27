using System;

namespace Order.Domain.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public string Number { get; }

        public OrderNotFoundException(string number) : base()
        {
            Number = number;
        }
    }
}
