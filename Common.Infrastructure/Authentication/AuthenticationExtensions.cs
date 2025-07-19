using System.Text;
using Common.Infrastructure.Authorization;
using Common.Infrastructure.Swagger;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Infrastructure.Authentication;

public static class AuthenticationExtensions
{
    /// <summary>
    ///     添加JWT认证配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static AuthenticationBuilder AddJwtAuthenticationConfiguration(this IServiceCollection services,
        JWTOptions options)
    {
        services.Configure<SwaggerGenOptions>(c => { c.AddAuthenticationHeader(); });
        services.AddAuthorization();
        services.AddAuthentication();
        //添加jwt的默认配置
        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwt =>
            {
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
                    RequireExpirationTime = true
                };
            });
    }

    /// <summary>
    ///     动态添加JWT认证配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtAuthenticationConfigurationoptions(this IServiceCollection services,
        JWTOptions options)
    {
        services.Configure<SwaggerGenOptions>(c => { c.AddAuthenticationHeader(); });
        //添加jwt的默认配置
        services.AddAuthenticationInternal(options);
        services.AddAuthorizationInternal();
        return services;
    }

    /// <summary>
    ///     Jwt认证添加
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    internal static IServiceCollection AddAuthenticationInternal(this IServiceCollection services, JWTOptions options)
    {
        services.AddAuthorization();

        services.AddAuthentication().AddJwtBearer(jwt =>
        {
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = options.Issuer,
                ValidAudience = options.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
                RequireExpirationTime = true
            };
        });
        //它使得在应用程序的任何地方（包括非控制器类）都能访问当前 HTTP 请求的 HttpContext；注入   IHttpContextAccessor
        services.AddHttpContextAccessor();
        return services;
    }

    internal static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }
}