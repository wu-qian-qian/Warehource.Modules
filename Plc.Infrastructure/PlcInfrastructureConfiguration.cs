using AutoMapper;
using Common.Presentation.Endpoints;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plc.Application;
using Plc.Application.Abstract;
using Plc.CustomEvents;
using Plc.Domain.S7;
using Plc.Infrastructure.Database;
using Plc.Infrastructure.db;
using Plc.Infrastructure.Service;
using Plc.Presentation.Custom;
using Plc.Presentation.Saga;
using AssemblyReference = Plc.Presentation.AssemblyReference;

namespace Plc.Infrastructure;

/// <summary>
///     Wcs的基础设施层注入
/// </summary>
public static class PlcInfrastructureConfiguration
{
    public static void AddPlcInfrastructureModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        AddRepository(services);
        AddEndPoint(services);
        services.TryAddSingleton<INetService>(sp =>
        {
            var sender = sp.CreateScope().ServiceProvider.GetService<ISender>();
            INetService netService = new S7NetService();
            netService.Initialization(sender);
            return netService;
        });
        services.AddDbContext<PlcDBContext>(options =>
        {
            var connStr = configuration.GetConnectionString("default");
            options.UseSqlServer(connStr, builder =>
                builder.MigrationsHistoryTable(Schemas.TableSchema + HistoryRepository.DefaultTableName));
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PlcDBContext>());
    }

    public static IServiceCollection AddRepository(this IServiceCollection service)
    {
        service.AddScoped<IS7NetManager, S7NetManager>();
        return service;
    }

    public static IServiceCollection AddEndPoint(this IServiceCollection services)
    {
        services.AddEndpoints(AssemblyReference.Assembly);
        return services;
    }

    public static void AddBehaviorModule(MediatRServiceConfiguration configuration)
    {
        ApplicationConfigurator.AddMediatR(configuration);
    }

    /// <summary>
    ///     公共事件注入
    /// </summary>
    /// <param name="registrationConfigurator"></param>
    public static void AddConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<ReadPlcEventConsumer<S7ReadPlcDataBlockIntegrationEvent>>();
        registrationConfigurator.AddConsumer<WritePlcEventConsumer<S7WritePlcDataBlockIntegrationEvent>>();
        registrationConfigurator.AddConsumer<PlcMapEventCommitConsumer>();
        //saga注入
        registrationConfigurator.AddSagaStateMachine<PlcMapSaga, PlcMapState>()
            .InMemoryRepository();
    }


    /// <summary>
    ///     automapper注入
    /// </summary>
    public static void AddAutoMapper(IMapperConfigurationExpression configurationExpression)
    {
        ApplicationConfigurator.AddAutoMapper(configurationExpression);
    }
}