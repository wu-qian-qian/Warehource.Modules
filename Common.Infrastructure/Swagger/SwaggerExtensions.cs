using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Infrastructure.Swagger;

public static class SwaggerExtensions
{
    /// <summary>
    ///     Swagger 配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="title"></param>
    /// <param name="version"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerConfigurator(this IServiceCollection services, string title = "API",
        string version = "v1",
        string desc = "API built using the modular monolith architecture.")
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = title,
                Version = version,
                Description = desc
            });

            options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
        });

        return services;
    }

    /// <summary>
    ///     为Swagger增加Authentication报文头
    /// </summary>
    /// <param name="c"></param>
    public static void AddAuthenticationHeader(this SwaggerGenOptions c)
    {
        c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
        {
            Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Authorization"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Authorization"
                    },
                    Scheme = "oauth2",
                    Name = "Authorization",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    }
}