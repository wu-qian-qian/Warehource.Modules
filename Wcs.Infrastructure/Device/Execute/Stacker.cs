using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Abstract;
using Wcs.Device.Config;
using Wcs.Device.DBEntity;

namespace Wcs.Infrastructure.Device.Execute;

public class Stacker : AbstractDevice<StackerConfig, StackerDBEntity>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public Stacker(string name, StackerConfig config, IServiceScopeFactory scopeFactory)
    {
        Name = name;
        Config = config;
        _scopeFactory = scopeFactory;
    }

    public override string Name { get; init; }

    public override StackerConfig Config { get; init; }

    /// <summary>
    /// </summary>
    /// <param name="scopeFactory"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override Task ExecuteAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}