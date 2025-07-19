using Common.Application.Caching;
using Common.Application.Encodings;
using Common.Application.Net.Http;
using Common.Application.QuartzJob;
using Common.Infrastructure.Authentication;
using Common.Infrastructure.Caching;
using Common.Infrastructure.Middleware;
using Common.Infrastructure.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace Common.Infrastructure;

public static class InfrastructureConfigurator
{
    /// <summary>
    ///     基本模块注册
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration, Action<HttpResponseMessage> policCallback = null)
    {
        // 配置异常拦截事件
        services.AddGlobalException();
        //配置SwaggerUI
        services.AddSwaggerConfigurator();
        // 配置编码服务
        services.AddScoped<ITokenService, TokenService>();
        //配置Quatrz Job
        services.AddQuatrzJobConfigurator();
        //配置传统鉴权JWT
        services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
        var options = configuration.GetSection("JWTOptions").Get<JWTOptions>();
        services.AddJwtAuthenticationConfigurationoptions(options);
        //注册缓存
        services.AddRedisCacheing("localhost:63w9");
        //HttpContext 注入
        services.AddHttpContextAccessor();
        // 添加HttpClient
        services.AddHttpClient(configuration, policCallback);
        return services;
    }


    /// <summary>
    ///     缓存注册
    /// </summary>
    /// <param name="services"></param>
    /// <param name="redisConnectionString"></param>
    /// <returns></returns>
    internal static IServiceCollection AddRedisCacheing(this IServiceCollection services, string redisConnectionString)
    {
        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.AddSingleton(connectionMultiplexer);
            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<ICacheService, CacheService>();
        return services;
    }

    /// <summary>
    ///     添加HttpClient
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    internal static IServiceCollection AddHttpClient(this IServiceCollection services, IConfiguration configuration,
        Action<HttpResponseMessage> policCallback)
    {
        services.Configure<HttpOptions>(configuration.GetSection("HttpOptions"));
        var httpOptions = configuration.GetSection("HttpOptions").Get<HttpOptions>();
        if (policCallback == null) httpOptions.PolicCallback = result => { Console.WriteLine("重试...."); };

        services.AddHttpClient(httpOptions);
        return services;
    }
}