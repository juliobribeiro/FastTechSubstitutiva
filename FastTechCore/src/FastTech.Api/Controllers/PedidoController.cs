using FastTech.Application.Interfaces;
using FastTech.Contracts;
using FastTech.Contracts.DataTransferObjects;
using MassTransit;

using Microsoft.AspNetCore.Mvc;

using static FastTech.Domain.Constants.AppConstants;

namespace FastTech.Api.Controllers
{
    [Route("Pedido")]
    public class PedidoController(
        ILogger<PedidoController> logger,
        IPedidoApplicationService pedidoApplicationService,
        IPublishEndpoint publishEndpoint,
        ISendEndpointProvider sendEndpointProvider
    ) : BaseController(logger)
    {
        private readonly IPedidoApplicationService _pedidoApplicationService = pedidoApplicationService;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly ISendEndpointProvider _sendEndpointProvider = sendEndpointProvider;

        /// <summary>
        /// Criar um novo Pedido
        /// </summary>
        /// <param name="listModel">Objeto com as propriedades para criar um novo Pedido</param>
        /// <returns>Status de envio à fila</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] List<BasicPedido> listModel)
        {
            try
            {
                var mensagem = new PedidoMessage
                {
                    Pedido = listModel
                };
                                
                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(
                    new Uri("queue:fasttech.pedido") // Nome da fila configurada no ReceiveEndpoint
                );

                // Publica a mensagem diretamente no RabbitMQ via MassTransit
                await sendEndpoint.Send(mensagem);


                return Ok(new { message = "Pedidos enviados para a fila com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
