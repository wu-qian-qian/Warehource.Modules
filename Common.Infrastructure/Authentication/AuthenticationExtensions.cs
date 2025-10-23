using System.Security.Claims;
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
        services.AddAuthorization(options =>
        {
            options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
            {
                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireClaim(ClaimTypes.NameIdentifier);
            });
        });
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
        // services.AddAuthorizationInternal();
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
        //可在其中对编写一些策略
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
            // 处理SignalR的特殊情况：Bearer Token可能通过查询字符串传递
            jwt.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    // 从查询字符串中获取access_token参数（SignalR默认将jwt字符放到access_token）
                    var accessToken = context.Request.Query["access_token"].ToString() ??
                                      context.Request.Headers.Authorization.ToString();
                    // 如果请求是针对SignalR集线器的
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken)) // 替换为你的Hub路径
                        //赋值这里鉴权中间件就可以获取到
                        context.Token = accessToken;
                    return Task.CompletedTask;
                }
            };
        });
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