﻿using Common.Application.QuartzJob;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Wcs.Domain.JobConfigs;
using Wcs.Shared;

namespace Wcs.Infrastructure.Job;

internal class JobOptions(IServiceScopeFactory serviceScope, IScheduler scheduler)
{
    public void Configure()
    {
        //   var r= scheduler.Context;
        var serviceProvider = serviceScope.CreateScope().ServiceProvider;

        var jobTypes = serviceProvider.GetKeyedService<Type[]>(Constant.JobKey);
        var jobService = serviceProvider.GetService<JobService>();
        var jobConfigs = jobService.GetAllJobConfigsAsync().GetAwaiter().GetResult();
        foreach (var item in jobConfigs)
        {
            var type = jobTypes.First(p => p.Name == item.JobType);
            AddJob(item, type);
        }
    }

    public void AddJob<TJob>(JobConfig jobConfig) where TJob : BaseJob
    {
        QuatrzJobExtensions.CreateJobDetail(typeof(TJob), jobConfig, scheduler);
    }

    public void AddJob(JobConfig jobConfig, Type jobType)
    {
        QuatrzJobExtensions.CreateJobDetail(jobType, jobConfig, scheduler);
    }
}