using Common.Application.Log;
using Common.Infrastructure;
using Common.Infrastructure.Log;
using Common.Infrastructure.Middleware;
using Common.Presentation.Endpoints;
using Common.Shared;
using Identity.Infrastructure;
using Plc.Infrastructure;
using Serilog;
using Warehource.Source;
using Wcs.Application;
using Wcs.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddSerilogConfiguratorCategory();

builder.Services.AddCors();


//服务通用模块
Action<HttpResponseMessage> policyCallback = result =>
{
    Log.Logger.ForCategory(LogCategory.Error).Information(
        "Http请求失败，状态码：{StatusCode}，请求地址：{RequestUri}，请求方法：{Method}",
        result.StatusCode, result.RequestMessage?.RequestUri, result.RequestMessage?.Method);
};

builder.Services.AddInfrastructure(builder.Configuration, policyCallback);
//单体模块注入

builder.Services.AddModules(builder.Configuration,
    //程序集
    [
        AssemblyReference.Assembly,
        Identity.Application.AssemblyReference.Assembly,
        Plc.Application.AssemblyReference.Assembly
    ],
    //模块的独立基础设施注入
    [
        WcsInfrastructureConfigurator.AddWcsInfrastructureModule,
        UserInfrastructureConfigurator.AddUserInfrastructureConfiguration,
        PlcInfrastructureConfigurator.AddPlcInfrastructureModule
    ],
    //模块化的MediatR管道注入
    [
        WcsInfrastructureConfigurator.AddBehaviorModule, PlcInfrastructureConfigurator.AddBehaviorModule
    ],
    //模块的的公共事件注入
    [
        WcsInfrastructureConfigurator.AddConsumers,
        PlcInfrastructureConfigurator.AddConsumers
    ]
    //模块化job的注入
    , [WcsInfrastructureConfigurator.AddJobs]
    //模块化aotoMapper注入
    , [
        WcsInfrastructureConfigurator.AddAutoMapper,
        UserInfrastructureConfigurator.AddAutoMapper,
        PlcInfrastructureConfigurator.AddAutoMapper
    ]
);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Initialization(); //在开发环境中应用数据库迁移
}

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseMiddleware<GlobalLogMiddleware>();
//app.UseMiddleware<GlobalResponseMiddleware>();
//app.UseMiddleware<GlobalEncodingRequestMiddleware>();
app.UseHttpsRedirection();


app.UseSerilogRequest();
app.AddGlobalExceptionMiddleware();
app.UseAuthentication();
app.UseAuthorization();


app.MapEndpoints();
app.Run();