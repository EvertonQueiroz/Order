using Order.Domain.Commands.Responses;
using Order.Domain.Queries.Responses;
using System.Collections.Generic;
using System.Linq;

namespace Order.API.Responses
{
    public class PedidoResponse
    {
        public string Pedido { get; }
        public IReadOnlyList<PedidoItemResponse> Itens { get; }

        public PedidoResponse(FindOrderByNumberResponse response)
        {
            Pedido = response.Number;
            Itens = response.Items.Select(item => new PedidoItemResponse(item)).ToList();
        }

        public PedidoResponse(CreateOrUpdateOrderResponse response)
        {
            Pedido = response.Number;
            Itens = response.Items.Select(item => new PedidoItemResponse(item)).ToList();
        }
    }

    public class PedidoItemResponse
    {

        public string Descricao { get; }
        public decimal PrecoUnitario { get; }
        public decimal Qtd { get; }

        public PedidoItemResponse(OrderItemReponse item)
        {
            Descricao = item.Description;
            PrecoUnitario = item.UnitPrice;
            Qtd = item.Amount;
        }

        public PedidoItemResponse(CreateOrUpdateOrderItemReponse item)
        {
            Descricao = item.Description;
            PrecoUnitario = item.UnitPrice;
            Qtd = item.Amount;
        }
    }
}
