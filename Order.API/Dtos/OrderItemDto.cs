using Order.Domain;
using Order.Domain.Commands.Requests;
using Order.Domain.Queries.Responses;
using System;

namespace Order.API.Dto
{
    public class OrderItemDto
    {
        public string Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Qtd { get; set; }

        public static explicit operator OrderItemDto(OrderItem item) =>
            new OrderItemDto
            {
                Descricao = item.Description,
                PrecoUnitario = item.UnitPrice,
                Qtd = item.Amount
            };

        public static explicit operator OrderItemCommand(OrderItemDto dto) =>
            new OrderItemCommand
            {
                Description = dto.Descricao,
                UnitPrice = dto.PrecoUnitario,
                Amount = dto.Qtd,
            };

        public static explicit operator OrderItemDto(OrderItemReponse response) =>
            new OrderItemDto
            {
                Descricao = response.Description,
                PrecoUnitario = response.UnitPrice,
                Qtd = response.Amount
            };
    }
}
