﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.API.Dto;
using Order.Domain.Commands.Requests;
using Order.Domain.Interfaces.Commands.Handlers;
using Order.Domain.Interfaces.Queries.Handlers;
using Order.Domain.Queries.Requests;
using System.Net;

namespace Order.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        /// <summary>
        /// Retorna todos os pedidos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("pedido")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetAll(
            [FromServices] IFindAllOrdersQueryHandler handler)
        {
            var result = handler.Handle(new FindAllOrdersRequest());



            return Ok(result);
        }

        /// <summary>
        /// Retorna o pedido conforme o número de pedido informado.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        [HttpGet("pedido/{pedido}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Get(
            [FromServices] IFindOrderByNumberQueryHandler handler,
            string pedido)
        {
            var result = handler.Handle(new FindOrderByNumberRequest(pedido));
            return Ok((OrderDto)result);
        }

        /// <summary>
        /// Registra o pedido com os dados informados.
        /// </summary>
        /// <param name="pedido"><sealso cref="OrderDto"></param>
        /// <returns></returns>
        [HttpPost("pedido")]
        [HttpPut("pedido")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult CreateOrUpdate(
            [FromServices] ICreateOrUpdateOrderCommandHandler handler,
            [FromBody] OrderDto dto)
        {
            var request = new CreateOrUpdateOrderRequest(dto.Pedido);
            dto.Itens.ForEach(item => request.AddItem(item.Descricao, item.PrecoUnitario, item.Qtd));

            var result = handler.Handle(request);
            return Created("", result);
        }

        /// <summary>
        /// Excluí o pedido conforme o número de pedido informado.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        [HttpDelete("pedido/{pedido}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Delete(
            [FromServices] IDeleteOrderCommandHandler handler,
            string pedido)
        {
            handler.Handle(new DeleteOrderRequest(pedido));
            return Ok();
        }

        /// <summary>
        /// Atualiza o status o pedido.
        /// </summary>
        /// <param name="pedido"><sealso cref="OrderStatusUpdatedDto"></param>
        /// <returns></returns>
        [HttpPost("status")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult UpdateStatus(
             [FromServices] IChangeStatusOrderCommandHandler handler,
             [FromBody] OrderStatusUpdatedDto dto)
        {
            var request = new ChangeStatusOrderRequest(dto.Pedido, dto.Status, dto.ItensAprovados, dto.ValorAprovado);

            var result = handler.Handle(request);
            return Ok(result);
        }
    }
}
