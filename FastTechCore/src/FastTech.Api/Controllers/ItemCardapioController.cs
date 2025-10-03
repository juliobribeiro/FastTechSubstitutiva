using FastTech.Application.DataTransferObjects;
using FastTech.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace FastTech.Api.Controllers
{
    /// <summary>
    /// ItemCardapio controller
    /// </summary>
    [Route("ItemCardapio")]
    public class ItemCardapioController(ILogger<ItemCardapioController> logger, IItemCardapioApplicationService ItemCardapioApplicationService) : BaseController(logger)
    {
        private readonly IItemCardapioApplicationService _ItemCardapioApplicationService = ItemCardapioApplicationService;

        /// <summary>
        /// Busca os itens do Cardápio
        /// </summary>
        /// <returns>Itens do Cardápio</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ItemCardapio), StatusCodes.Status200OK)]
        public async Task<object> GetAll()
        {
            try
            {
                var ItemCardapio = await _ItemCardapioApplicationService.GetAll();
                return Ok(ItemCardapio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Criar um novo Item do Cardapio
        /// </summary>
        /// <param name="model">Objeto com as propriedades para criar um novo ItemCardapio</param>
        /// <returns>Um objeto do ItemCardapio criado</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ItemCardapio), StatusCodes.Status200OK)]
        public async Task<object> Create([FromBody] BasicItemCardapio model)
        {
            try
            {
                var ItemCardapio = await _ItemCardapioApplicationService.Add(model);
                return Ok(ItemCardapio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Editar um Item do Cardapio
        /// </summary>
        /// <param name="model">Objeto com as propriedades para editar um Item do Cardapio</param>
        /// <returns>Um objeto do Item do Cardapio criado</returns>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ItemCardapio), StatusCodes.Status200OK)]
        public async Task<object> Update([FromBody] ItemCardapio model)
        {
            try
            {
                var ItemCardapio = await _ItemCardapioApplicationService.Update(model);
                return Ok(ItemCardapio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
