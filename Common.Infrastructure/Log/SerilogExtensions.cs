using Common.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;

namespace Common.Infrastructure.Log;

public static class SerilogExtensions
{
    public static WebApplicationBuilder AddSerilogConfigurator(this WebApplicationBuilder builder)
    {
        //配置日志
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File("logs/Info/log.txt",
                LogEventLevel.Information,
                rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        //这里用替换原生日志
        builder.Host.UseSerilog();
        return builder;
    }

    public static WebApplicationBuilder AddSerilogConfiguration(this WebApplicationBuilder builder)
    {
        //配置日志
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .MinimumLevel.Override("Quartz", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            // 系统日志
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") == false)
                .WriteTo.Console()
                .WriteTo.File("Logs/systems/system-.log", rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}"))
            // 业务日志
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Category"].ToString() == "Business")
                .WriteTo.File("Logs/business/business-.log", rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    @"{{
    ""Timestamp"": ""{Timestamp:o}"",
    ""Level"": ""{Level}"",
 ""DeviceName"": ""{DeviceName}"",
    ""SerialNum"": ""{SerialNum}"",
    ""Message"": ""{Message}""
}}{NewLine}"))
            //Http日志
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Category"].ToString() == "Http")
                .WriteTo.File("Logs/Https/http-.log", rollingInterval: RollingInterval.Day,
                    outputTemplate: @"{{
    ""Timestamp"": ""{Timestamp:o}"",
    ""Level"": ""{Level}"",
 ""IP"": ""{IP}"",
    ""URL"": ""{URL}"",
    ""Request"": ""{Request}"",
    ""Response"": ""{Response}"",
    ""TimeUsed"": {TimeUsed}
}}{NewLine}")
            )
            //error日志
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Category"].ToString() == "Error")
                .WriteTo.File("Logs/Error/error-.log", rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"))

            //Event日志
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Event"].ToString() == "Event")
                .WriteTo.File("Logs/Event/event-.log", rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"))
            //网络链接日志
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Category") &&
                                             e.Properties["Category"].ToString() == "Net")
                .WriteTo.File("Logs/Nets/net-.log", rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"))
            .CreateLogger();
        //这里用替换原生日志
        builder.Host.UseSerilog();
        return builder;
    }

    public static WebApplication UseSerilogRequest(this WebApplication app)
    {
        app.UseMiddleware<GlobalLogMiddleware>();
        //app.UseMiddleware<GlobalResponseMiddleware>();
        //app.UseMiddleware<GlobalEncodingRequestMiddleware>();
        //使用Serilog中间件
        // app.UseSerilogRequestLogging();
        return app;
    }
}