using Microsoft.AspNetCore.SignalR.Client;
using UI.Components;
using UI.Helper;
using UI.Service.DeviceService;
using UI.Service.ExecuteNodeService;
using UI.Service.HomeService;
using UI.Service.IdentityService;
using UI.Service.JobService;
using UI.Service.MainService;
using UI.Service.PlcSevice;
using UI.Service.RegionService;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//ant blazor 注入
builder.Services.AddAntDesign();
builder.Services.AddScoped<IMainService, MainService>();

builder.Services.AddScoped<IMainService, MainService>();

builder.Services.AddScoped<IHomeService, HomeService>();

builder.Services.AddScoped<IJobService, JobService>();

builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddScoped<IPlcService, PlcService>();

builder.Services.AddScoped<IExecuteNodeService, ExecuteNodeService>();

builder.Services.AddScoped<IRegionService, RegionService>();

builder.Services.AddScoped<IDeviceService, DeviceService>();
var path = builder.Configuration.GetSection("WcsService:ServiceUrl").Value;
var uri = new Uri(path);
// httpClient 注入
builder.Services.AddHttpClient("ServiceClient", client => { client.BaseAddress = uri; });
builder.Services.AddSingleton<HttpClientFactory>(sp =>
{
    var basehttpFactory = sp.GetService<IHttpClientFactory>();
    var clientFactory = new HttpClientFactory("ServiceClient", basehttpFactory);
    return clientFactory;
});
builder.Services.AddScoped<HubConnection>(sp =>
{
    var mainService = sp.GetRequiredService<IMainService>();
    var hubConnection = new HubConnectionBuilder()
        .WithUrl("path/wcshub", options => { options.AccessTokenProvider = () => mainService.GetToken(); })
        .Build();
    hubConnection.StartAsync().GetAwaiter().GetResult();
    return hubConnection;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();