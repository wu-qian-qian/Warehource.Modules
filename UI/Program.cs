using UI.Components;
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
//ע��ant
builder.Services.AddAntDesign();
//ע��HttpClient
builder.Services.AddScoped<IMainService, MainService>();

builder.Services.AddScoped<IHomeService, HomeService>();

builder.Services.AddScoped<IJobService, JobService>();

builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddScoped<IPlcService, PlcService>();

builder.Services.AddScoped<IExecuteNodeService, ExecuteNodeService>();

builder.Services.AddScoped<IRegionService, RegionService>();

builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddHttpClient();

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