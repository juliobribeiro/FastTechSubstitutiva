using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static FastTechKitchen.Domain.Constants.AppConstants;

namespace FasTechKitchen.Api.Controllers
{
    [Route("Pedido")]
    public class PedidoController(
        ILogger<PedidoController> logger,
        IPedidoApplicationService pedidoApplicationService
    ) : BaseController(logger)
    {
        private readonly IPedidoApplicationService _pedidoApplicationService = pedidoApplicationService;

        /// <summary>
        /// Busca os Pedidos Ativos
        /// </summary>
        [HttpGet]
        [Authorize(Policy = Policies.Funcionario)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Pedido>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pedidos = await _pedidoApplicationService.FindBy(p => p.Ativo);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Editar um Pedido
        /// </summary>
        /// <param name="model">Objeto com as propriedades para editar um Pedido</param>
        /// <returns>Pedido atualizado</returns>
        [HttpPut]
        [Authorize(Policy = Policies.Funcionario)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Pedido), StatusCodes.Status200OK)]
        public async Task<object> Update([FromBody] Pedido model)
        {
            try
            {
                var pedido = await _pedidoApplicationService.Update(model);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
