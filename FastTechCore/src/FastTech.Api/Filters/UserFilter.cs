using FastTech.Domain.Entities;
using FastTech.Domain.Settings;
using FastTech.Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace FastTech.Api.Filters;

public class UserFilter(UserData userData, IOptions<TokenSettings> options) : IAuthorizationFilter
{
    private readonly UserData _userData = userData;
    private readonly TokenSettings _settings = options.Value;

    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.Filters.Any(f => f is SkipUserFilterAttribute))
            return;

        var user = context.HttpContext.User;
        var userId = user?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var token = context.HttpContext.Request.Headers.Authorization.FirstOrDefault();
        if (token != null)
        {
            _userData.Set(TokenHelper.GetUserData(token, _settings.Key));
            var timeUntilExpiration = TokenHelper.GetTimeUntilExpiration(token, _settings.Key);

            if (context.HttpContext.User?.Claims?.Count() <= 0)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
        else
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
