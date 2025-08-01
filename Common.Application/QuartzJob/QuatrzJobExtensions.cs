using Common.Shared;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Common.Application.QuartzJob;

public static class QuatrzJobExtensions
{
    public static IServiceCollection AddQuatrzJobConfigurator(this IServiceCollection services)
    {
        services.AddSingleton<QuartzJobListener>();
        services.AddQuartz(configurator =>
        {
            configurator.SchedulerId = $"{JobConstant.JobScheduler.GetHashCode()}";
            configurator.SchedulerName = $"{JobConstant.JobScheduler}";
            configurator.MisfireThreshold = TimeSpan.FromSeconds(8);
            configurator.InterruptJobsOnShutdownWithWait = true;
            configurator.UseDefaultThreadPool(tp =>
            {
                tp.MaxConcurrency = 10; // 设置最大并发数
            });
            configurator.AddSchedulerListener<QuartzJobListener>();
        }).AddSingleton<IScheduler>(sp =>
        {
            var schedulerFactory = sp.GetService<ISchedulerFactory>();
            var scheduler = schedulerFactory.GetScheduler().Result;
            return scheduler;
        });
        services.AddQuartzHostedService(configure =>
        {
            configure.AwaitApplicationStarted = true;
            configure.WaitForJobsToComplete = true;
        });

        return services;
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TJob"></typeparam>
    /// <param name="options"></param>
    /// <param name="quartzJobOptins"></param>
    public static void AddQuqartzJob<TJob>(this QuartzOptions options, QuartzJobOptins quartzJobOptins)
        where TJob : IJob
    {
        var jobName = typeof(TJob).FullName!;
        options
            .AddJob<TJob>(configure =>
            {
                configure.WithIdentity(jobName);
                var detail = configure.Build();
                var keyValues = new JobDataMap();
                keyValues.Add(quartzJobOptins.TimeoutSeconds, quartzJobOptins.TimeOut);
                configure.UsingJobData(keyValues);
            })
            .AddTrigger(configure =>
            {
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(3).RepeatForever()).StartNow();
            });
    }

    /// <summary>
    ///     通过type 来实现job的生成
    /// </summary>
    /// <param name="jobType"></param>
    /// <param name="jobConfig"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static void CreateJobDetail(Type jobType, dynamic jobConfig, IScheduler scheduler)
    {
        if (typeof(BaseJob).IsAssignableFrom(jobType) == false)
            throw new ArgumentException("job必须继承自BaseJob");
        JobBuilder jobBuilder = JobBuilder.Create(jobType)
            .WithIdentity(jobConfig.Name)
            .WithDescription(jobConfig.Description);

        jobBuilder.UsingJobData(jobConfig.TimeoutSeconds, jobConfig.TimeOut);
        var jobDetail = jobBuilder.Build();

        var trigger = TriggerBuilder.Create()
            .WithSimpleSchedule(p
            => { p.WithInterval(new TimeSpan(0, 0, 0, 0, jobConfig.Timer)).RepeatForever(); })
            .Build();
        scheduler.ScheduleJob(jobDetail, trigger);

        if (jobConfig.IsStart == false)
            scheduler.PauseJob(new JobKey(jobConfig.Name));
        else
            scheduler.ResumeJob(new JobKey(jobConfig.Name));
    }
}