using Order.Domain.Commands.Requests;
using Order.Domain.Commands.Responses;
using Order.Domain.Exceptions;
using Order.Domain.Interfaces.Commands.Handlers;
using Order.Domain.Interfaces.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Domain.Commands.Handlers
{
    public class ChangeStatusOrderCommandHandler : IChangeStatusOrderCommandHandler
    {
        private IOrderRepository _orderRepository;

        public ChangeStatusOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException();
        }

        public ChangeStatusOrderResponse Handle(ChangeStatusOrderRequest command)
        {
            if (!command.IsValid())
                throw new InvalidRequestException(command.Errors);

            var order = _orderRepository.Get(command.Number);

            var response = new ChangeStatusOrderResponse(command.Number);
            if (order is null)
            {
                response.AddStatus("CODIGO_PEDIDO_INVALIDO");
                return response;
            }


            if (command.Status.Equals("REPROVADO", StringComparison.CurrentCultureIgnoreCase))
                response.AddStatus("REPROVADO");
            else if (command.Status.Equals("APROVADO", StringComparison.CurrentCultureIgnoreCase))
                response.AddStatus(Approve(order, command.ApprovedAmount, command.ApprovedValue));
            else
                throw new InvalidOperationException();

            return response;
        }

        private IEnumerable<string> Approve(Order order, decimal approvedAmount, decimal approvedValue)
        {
            var totalOrderAmount = order.Items.Sum(item => item.Amount);
            var totalOrderValue = order.Items.Sum(item => item.UnitPrice * item.Amount);

            var response = new List<string>();

            if (totalOrderAmount == approvedAmount && totalOrderValue == approvedValue)
            {
                response.Add("APROVADO");
                return response;
            }

            if (approvedAmount < totalOrderAmount)
                response.Add("APROVADO_QTD_A_MENOR");

            if (approvedAmount > totalOrderAmount)
                response.Add("APROVADO_QTD_A_MAIOR");

            if (approvedValue < totalOrderValue)
                response.Add("APROVADO_VALOR_A_MENOR");

            if (approvedAmount > totalOrderAmount)
                response.Add("APROVADO_VALOR_A_MAIOR");

            return response;
        }
    }
}
