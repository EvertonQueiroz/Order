using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.API.Dto;
using Order.Domain.Commands.Requests;
using Order.Domain.Exceptions;
using Order.Domain.Interfaces.Commands.Handlers;
using Order.Domain.Interfaces.Queries.Handlers;
using Order.Domain.Queries.Requests;
using System;

namespace Order.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger _logger;

        public PedidoController(ILogger<PedidoController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Retorna todos os pedidos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("pedido")]
        public IActionResult GetAll(
            [FromServices] IFindAllOrdersQueryHandler handler)
        {
            try
            {
                var result = handler.Handle(new FindAllOrdersRequest());

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }

        /// <summary>
        /// Retorna o pedido conforme o número de pedido informado.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        [HttpGet("pedido/{pedido}")]
        public IActionResult Get(
            [FromServices] IFindOrderByNumberQueryHandler handler,
            string pedido)
        {
            try
            {
                var result = handler.Handle(new FindOrderByNumberRequest(pedido));

                return Ok((OrderDto)result);
            }
            catch (Exception ex)
            {
                if (ex is OrderNotFoundException)
                    return NotFound();

                _logger.LogError(ex.Message);
                return Problem();
            }
        }

        /// <summary>
        /// Registra o pedido com os dados informados.
        /// </summary>
        /// <param name="pedido"><sealso cref="OrderDto"></param>
        /// <returns></returns>
        [HttpPost("pedido")]
        [HttpPut("pedido")]
        public IActionResult CreateOrUpdate(
            [FromServices] ICreateOrUpdateOrderCommandHandler handler,
            [FromBody] OrderDto dto)
        {
            try
            {
                var request = new CreateOrUpdateOrderRequest(dto.Pedido);
                dto.Itens.ForEach(item => request.AddItem(item.Descricao, item.PrecoUnitario, item.Qtd));

                var result = handler.Handle(request);
                return Created("", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }

        /// <summary>
        /// Excluí o pedido conforme o número de pedido informado.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        [HttpDelete("pedido/{pedido}")]
        public IActionResult Delete(
            [FromServices] IDeleteOrderCommandHandler handler,
            string pedido)
        {
            try
            {
                handler.Handle(new DeleteOrderRequest(pedido));
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is OrderNotFoundException)
                    return NotFound();

                _logger.LogError(ex.Message);
                return Problem();
            }
        }

        /// <summary>
        /// Atualiza o status o pedido.
        /// </summary>
        /// <param name="pedido"><sealso cref="OrderStatusUpdatedDto"></param>
        /// <returns></returns>
        [HttpPost("status")]
        public IActionResult UpdateStatus(
             [FromServices] IChangeStatusOrderCommandHandler handler,
             [FromBody] OrderStatusUpdatedDto dto)
        {
            try
            {
                var request = new ChangeStatusOrderRequest(dto.Pedido, dto.Status, dto.ItensAprovados, dto.ValorAprovado);

                var result = handler.Handle(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
    }
}
