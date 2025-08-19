using Microsoft.Extensions.DependencyInjection;

namespace Wcs.Application.Abstract;

public abstract class BaseDependy
{
    protected readonly IServiceScopeFactory _scopeFactory;

    protected BaseDependy(IServiceScopeFactory serviceScopeFactory)
    {
        _scopeFactory = serviceScopeFactory;
    }
}