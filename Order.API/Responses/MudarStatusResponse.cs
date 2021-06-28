using Order.Domain.Commands.Responses;
using System.Collections.Generic;
using System.Linq;

namespace Order.API.Responses
{
    public class MudarStatusResponse
    {
        public string Pedido { get; }
        public IReadOnlyList<string> Status { get; }

        public MudarStatusResponse(ChangeStatusOrderResponse response)
        {
            Pedido = response.Number;
            Status = response.Status.ToList();
        }
    }
}
