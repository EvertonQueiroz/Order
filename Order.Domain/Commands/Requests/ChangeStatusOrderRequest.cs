using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Domain.Commands.Requests
{
    public class ChangeStatusOrderRequest
    {
        public string Number { get; private set; }
        public string Status { get; private set; }
        public decimal ApprovedAmount { get; private set; }
        public decimal ApprovedValue { get; private set; }

        private readonly List<string> _errors;
        public IReadOnlyCollection<string> Errors => _errors.ToList();

        public ChangeStatusOrderRequest(string number, string status, decimal approvedAmount, decimal approvedValue)
        {
            Number = number;
            Status = status;
            ApprovedAmount = approvedAmount;
            ApprovedValue = approvedValue;

            _errors = new List<string>();
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Number))
                _errors.Add("Número do pedido é requerido.");

            if (Status is not null && Status.Equals("APROVADO", StringComparison.CurrentCultureIgnoreCase))
            {
                if (ApprovedAmount <= 0)
                    _errors.Add("A quantidade aprovada deve ser maior que zero.");

                if (ApprovedValue <= 0)
                    _errors.Add("O valor aprovado deve ser maior que zero.");
            }
            else if (Status is not null && Status.Equals("REPROVADO", StringComparison.CurrentCultureIgnoreCase))
            {
                if (ApprovedAmount != 0)
                    _errors.Add("A quantidade aprovada deve ser zero quando o status for \"REPROVADO\".");

                if (ApprovedValue != 0)
                    _errors.Add("O valor aprovado deve ser zero quando o status for \"REPROVADO\".");
            }
            else
            {
                _errors.Add("O Status deve ser \"APROVADO\" ou \"REPROVADO\".");
            }

            return !_errors.Any();
        }
    }
}
