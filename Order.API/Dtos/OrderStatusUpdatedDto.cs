using Order.Domain.Commands.Requests;

namespace Order.API.Dto
{
    public class OrderStatusUpdatedDto
    {
        public string Status { get; set; }
        public decimal ItensAprovados { get; set; }
        public decimal ValorAprovado { get; set; }
        public string Pedido { get; set; }

        public static explicit operator ChangeStatusOrderRequest(OrderStatusUpdatedDto dto) =>
            new ChangeStatusOrderRequest
            {
                Number = dto.Pedido,
                Value = dto.ValorAprovado,
                Amount = dto.ItensAprovados,
                Status = dto.Status
            };
    }
}
