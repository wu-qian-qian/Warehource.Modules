using System.Reflection;
using AutoMapper;
using Common.Application.Net;
using Common.Presentation.Endpoints;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wcs.Application;
using Wcs.Application.Abstract;
using Wcs.Application.S7Plc;
using Wcs.Domain.JobConfigs;
using Wcs.Infrastructure.Database;
using Wcs.Infrastructure.ReadPlcJob;
using Wcs.Shared;
using AssemblyReference = Wcs.Domain.AssemblyReference;

namespace Wcs.Infrastructure;

/// <summary>
///     һЩע������
/// </summary>
public static class WcsInfrastructureConfigurator
{
    public static void AddWcsInfrastructureModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        AddRepository(services);
        AddEndPoint(services);
        services.TryAddSingleton<INetService, S7NetService>();
        services.AddScoped<JobService>();
        services.AddDbContext<WCSDBContext>(options =>
        {
            var connStr = configuration.GetConnectionString("default");
            options.UseSqlServer(connStr, builder =>
                builder.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.TableSchema));
        });
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<WCSDBContext>());
    }

    public static IServiceCollection AddRepository(this IServiceCollection service)
    {
        var domainTypes = AssemblyReference.Assembly.GetTypes();

        var infrastructureiTypes = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var item in domainTypes)
            if (item.Name.EndsWith("Repository"))
                if (infrastructureiTypes.Any(p => item.IsAssignableFrom(p)))
                {
                    var repositoryType = infrastructureiTypes.First(p => item.IsAssignableFrom(p));
                    service.AddScoped(item, repositoryType);
                }

        return service;
    }

    public static IServiceCollection AddEndPoint(this IServiceCollection services)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        return services;
    }

    public static void AddBehaviorModule(MediatRServiceConfiguration configuration)
    {
        ApplicationConfigurator.AddMediatR(configuration);
    }

    public static void AddConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        ApplicationConfigurator.AddCustom(registrationConfigurator);
    }

    /// <summary>
    ///     ������ҵ����
    /// </summary>
    /// <param name="service"></param>
    public static void AddJobs(IServiceCollection service)
    {
        service.TryAddSingleton<JobOptions>();
        Type[] jobtypes = { typeof(ReadPlcJob.ReadPlcJob) };
        service.AddKeyedSingleton(Constant.JobKey, jobtypes);
    }

    public static void LoadJob(this IServiceProvider provider)
    {
        provider.GetRequiredService<JobOptions>().Configure();
    }

    /// <summary>
    ///     automapper�Զ�ӳ������
    /// </summary>
    public static void AddAutoMapper(IMapperConfigurationExpression configurationExpression)
    {
        ApplicationConfigurator.AddAutoMapper(configurationExpression);
    }
}