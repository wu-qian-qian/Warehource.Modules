using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Authorization;

/// <summary>
///     用来配置自定义声明转换
/// </summary>
/// <param name="serviceScopeFactory"></param>
internal sealed class CustomClaimsTransformation(IServiceScopeFactory serviceScopeFactory) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        using var scope = serviceScopeFactory.CreateScope();

        // principal.Claims.First().Value
        //var claimsIdentity = new ClaimsIdentity();


        //claimsIdentity.AddClaim(new Claim("Permission", "123"));

        //principal.AddIdentity(claimsIdentity);

        return principal;
    }
}