using Order.Domain.Commands.Requests;
using Order.Domain.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.API.Dto
{
    public class OrderDto
    {
        public string Pedido { get; set; }

        public List<OrderItemDto> Itens { get; set; }

        public OrderDto()
        {
            Itens = new List<OrderItemDto>();
        }

        public static explicit operator OrderDto(Domain.Order order) =>
            new OrderDto
            {
                Pedido = order.Number,
                Itens = order.Items.Select(item => (OrderItemDto)item).ToList()
            };

        public static explicit operator CreateOrUpdateOrderRequest(OrderDto dto) =>
            new CreateOrUpdateOrderRequest
            {
                Number = dto.Pedido,
                Items = dto.Itens.Select(item => (OrderItemCommand)item).ToList()
            };

        public static explicit operator OrderDto(FindOrderByNumberResponse response) =>
            new OrderDto
            {
                Pedido = response.Number,
                Itens = response.Items.Select(item => (OrderItemDto)item).ToList()
            };
    }
}
