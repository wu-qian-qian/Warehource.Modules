using AutoMapper;
using Common.Presentation.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application;
using User.Application.Abstract;
using User.Domain;
using User.Infrastructure.Database;
using User.Infrastructure.Role;
using User.Infrastructure.User;
using AssemblyReference = User.Presentation.AssemblyReference;

namespace User.Infrastructure;

public static class UserInfrastructureConfigurator
{
    public static void AddUserInfrastructureConfigurator(this IServiceCollection services,
        IConfiguration configuration)
    {
        AddRepository(services);
        services.AddScoped<UserManager>();
        AddEndPoint(services);
        services.AddDbContext<UserDBContext>(options =>
        {
            var connStr = configuration.GetConnectionString("default");
            options.UseSqlServer(connStr, builder =>
                builder.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.TableSchema));
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