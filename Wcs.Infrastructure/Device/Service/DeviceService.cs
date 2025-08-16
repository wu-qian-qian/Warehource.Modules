using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Abstract;

namespace Wcs.Infrastructure.Device.Service;

internal class DeviceService : IDeviceService
{
    private readonly IServiceScopeFactory _scopeFactory;
}