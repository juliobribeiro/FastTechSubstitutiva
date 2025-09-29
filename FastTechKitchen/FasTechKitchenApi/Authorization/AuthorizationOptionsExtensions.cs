using FastTechKitchen.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace FasTechKitchen.Api.Authorization;

public static class AuthorizationOptionsExtensions
{
    public static AuthorizationOptions AddPolicyWithPermission(this AuthorizationOptions options, string policyName, PermissionLevel permission)
    {
        options.AddPolicy(policyName, policy => policy.Requirements.Add(new RolesRequirement(permission)));
        return options;
    }
}