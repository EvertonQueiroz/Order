using System.Collections.Generic;

namespace Order.API.Requests
{
    public class PedidoRequest
    {
        public string Pedido { get; set; }
        public List<PedidoItemRequest> Itens { get; set; }
    }
}
