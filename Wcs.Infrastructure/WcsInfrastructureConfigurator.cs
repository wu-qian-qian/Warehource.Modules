using AutoMapper;
using Common.Presentation.Endpoints;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wcs.Application;
using Wcs.Application.Abstract;
using Wcs.Application.SignalR;
using Wcs.CustomEvents;
using Wcs.Domain.Device;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.JobConfigs;
using Wcs.Domain.Plc;
using Wcs.Domain.Region;
using Wcs.Domain.Task;
using Wcs.Infrastructure.Database;
using Wcs.Infrastructure.DB.Device;
using Wcs.Infrastructure.DB.ExecuteNodePath;
using Wcs.Infrastructure.DB.JobConfig;
using Wcs.Infrastructure.DB.PlcMap;
using Wcs.Infrastructure.DB.Region;
using Wcs.Infrastructure.DB.WcsTask;
using Wcs.Infrastructure.Job.JobItems;
using Wcs.Infrastructure.Job.Options;
using Wcs.Infrastructure.Job.Service;
using Wcs.Infrastructure.SignalR;
using Wcs.Presentation.Custom;
using Wcs.Shared;
using AssemblyReference = Wcs.Presentation.AssemblyReference;

namespace Wcs.Infrastructure;

/// <summary>
///     Wcs的基础设施层注入
/// </summary>
public static class WcsInfrastructureConfigurator
{
    public static void AddWcsInfrastructureModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        AddRepository(services);
        AddEndPoint(services);
        services.AddDbContext<WCSDBContext>(options =>
        {
            var connStr = configuration.GetConnectionString("default");
            options.UseSqlServer(connStr, builder =>
                builder.MigrationsHistoryTable(Schemas.TableSchema + HistoryRepository.DefaultTableName));
        });
        //获取到当前使用的生命周期，在从中获取到dbContext
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<WCSDBContext>());
        services.AddSignalRConfiguration();
    }

    public static IServiceCollection AddRepository(this IServiceCollection service)
    {
        service.AddScoped<IJobService, JobService>();
        service.AddScoped<IJobConfigRepository, JobConfigRepository>();
        service.AddScoped<IRegionRepository, RegionRepository>();
        service.AddScoped<IWcsTaskRepository, WcsTaskRepository>();
        service.AddScoped<IExecuteNodeRepository, ExecuteNodeRepository>();
        service.AddScoped<IDeviceRepository, DeviceRepository>();
        service.AddScoped<IPlcMapRepository, PlcMapRepository>();
        return service;
    }

    public static IServiceCollection AddSignalRConfiguration(this IServiceCollection services)
    {
        // 注册SignalR服务
        services.AddSignalR();
        // 可以在这里添加其他SignalR相关的配置或服务
        services.AddScoped<IHubManager, HubManager>();
        return services;
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
    ///     公共事件消费者
    /// </summary>
    /// <param name="registrationConfigurator"></param>
    public static void AddConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<PlcMapDataIntegrationEventConsumer>();
    }

    /// <summary>
    ///     job添加
    /// </summary>
    /// <param name="service"></param>
    public static void AddJobs(IServiceCollection service)
    {
        service.TryAddSingleton<JobOptions>();
        Type[] jobtypes = { typeof(ReadPlcJob) };
        service.AddKeyedSingleton(Constant.JobKey, jobtypes);
    }

    /// <summary>
    ///     加载添加的job
    /// </summary>
    /// <param name="provider"></param>
    public static void LoadJob(this IServiceProvider provider)
    {
        provider.GetRequiredService<JobOptions>().Configure();
    }

    /// <summary>
    ///     automapper注入
    /// </summary>
    public static void AddAutoMapper(IMapperConfigurationExpression configurationExpression)
    {
        ApplicationConfigurator.AddAutoMapper(configurationExpression);
    }
}