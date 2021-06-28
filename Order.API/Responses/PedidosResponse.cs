using Order.Domain.Queries.Responses;
using System.Collections.Generic;
using System.Linq;

namespace Order.API.Responses
{
    public class PedidosResponse
    {
        public IReadOnlyList<PedidoResumo> Pedidos { get; }

        public PedidosResponse(FindAllOrdersResponse response)
        {
            Pedidos = response.Orders.Select(order => new PedidoResumo(order)).ToList();
        }
    }

    public class PedidoResumo
    {
        public string Pedido { get; }
        public decimal ValorTotal { get; }
        public decimal Itens { get; }

        public PedidoResumo(OrderSummary summary)
        {
            Pedido = summary.Number;
            ValorTotal = summary.TotalValue;
            Itens = summary.TotalAmount;
        }
    }
}
