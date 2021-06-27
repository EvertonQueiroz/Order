using System;

namespace Order.Domain.Commands.Requests
{
    public class ChangeStatusOrderRequest
    {
        public string Number { get; private set; }
        public string Status { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Value { get; private set; }

        public ChangeStatusOrderRequest(string number, string status, decimal amount, decimal value)
        {
            Number = number;
            Status = status;
            Amount = amount;
            Value = value;
        }

        internal bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
