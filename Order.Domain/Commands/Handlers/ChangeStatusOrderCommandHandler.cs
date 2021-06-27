using Order.Domain.Commands.Requests;
using Order.Domain.Commands.Responses;
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
            _orderRepository = orderRepository;
        }

        public ChangeStatusOrderResponse Handle(ChangeStatusOrderRequest command)
        {
            var order = _orderRepository.Get(command.Number);

            var response = new ChangeStatusOrderResponse(command.Number);
            if (order is null)
            {
                response.AddStatus("CODIGO_PEDIDO_INVALIDO");
                return response;
            }

            switch (command.Status)
            {
                case "REPROVADO":
                    response.AddStatus("REPROVADO");
                    return response;

                case "APROVADO":
                    response.AddStatus(Approve(order, command.Amount, command.Value));
                    return response;

                default:
                    throw new InvalidOperationException();
            }
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
