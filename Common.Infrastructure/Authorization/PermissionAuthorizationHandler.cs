using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var permissions = GetPermissions(context.User);

        if (permissions.Contains(requirement.Permission)) context.Succeed(requirement);
        if (context.Resource is HttpContext httpContext)
        {
        }

        return Task.CompletedTask;
    }

    public static HashSet<string> GetPermissions(ClaimsPrincipal? principal)
    {
        var permissionClaims = principal?.FindAll("Permission") ??
                               throw new Exception("Permissions are unavailable");

        return permissionClaims.Select(c => c.Value).ToHashSet();
    }
}