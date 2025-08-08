using AutoMapper;
using Common.Presentation.Endpoints;
using Identity.Application;
using Identity.Application.Abstract;
using Identity.Domain;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.Role;
using Identity.Infrastructure.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AssemblyReference = Identity.Presentation.AssemblyReference;

namespace Identity.Infrastructure;

public static class UserInfrastructureConfigurator
{
    public static void AddUserInfrastructureConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        AddRepository(services);
        services.AddScoped<UserManager>();
        AddEndPoint(services);
        services.AddDbContext<UserDBContext>(options =>
        {
            var connStr = configuration.GetConnectionString("default");
            options.UseSqlServer(connStr, builder =>
                builder.MigrationsHistoryTable(Schemas.TableSchema + HistoryRepository.DefaultTableName));
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UserDBContext>());
    }

    public static IServiceCollection AddRepository(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IRoleRepository, RoleRepository>();
        return service;
    }

    public static IServiceCollection AddEndPoint(this IServiceCollection services)
    {
        services.AddEndpoints(AssemblyReference.Assembly);
        return services;
    }

    /// <summary>
    ///     automapper自动映射配置
    /// </summary>
    public static void AddAutoMapper(IMapperConfigurationExpression configurationExpression)
    {
        ApplicationConfigurator.AddAutoMapper(configurationExpression);
    }
}