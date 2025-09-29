using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FastTechKitchen.Domain.Constants.AppConstants;

namespace FasTechKitchen.Api.Controllers;

/// <summary>
/// User controller
/// </summary>
[Route("users")]
[ApiController]
public class UserController(ILogger<UserController> logger, IUserApplicationService userApplicationService) : BaseController(logger)
{
    private readonly IUserApplicationService _userApplicationService = userApplicationService;

    /// <summary>
    /// Criar um novo usuário
    /// </summary>
    /// <param name="user">Objeto com as propriedades para criar um novo usuário</param>
    /// <returns>Um objeto do usuário criado</returns>
    [HttpPost]
    [Authorize(Policy = Policies.Gerente)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    public async Task<object> Create([FromBody] BasicUser user)
    {
        try
        {
            var entity = await _userApplicationService.Add(user);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}