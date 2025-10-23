using Common.Domain.State;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application.StateMachine;

public class StateMachineManager : IStateMachineManager
{
    private readonly Dictionary<string, Type> _dicState;

    private readonly IServiceProvider _serviceProvider;

    public StateMachineManager(IServiceScopeFactory serviceScopeFactory)
    {
        _dicState = new Dictionary<string, Type>();
        _serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
    }

    public async ValueTask NextStatus(string key, string json, CancellationToken token = default)
    {
        var statusType = _dicState[key];
        if (statusType != null)
        {
            var scop = _serviceProvider.CreateScope();
            var status = scop.ServiceProvider.GetService(statusType) as IStateMachine;
            if (token.IsCancellationRequested)
                token.ThrowIfCancellationRequested();
            await status.HandlerAsync(json, token);
        }
        else
        {
            throw new ArgumentNullException("状态，没有被正确初始化");
        }
    }

    public void AddStates(string key, Type status)
    {
        if (_dicState == null) throw new AggregateException("未实例化数据结构");
        if (_dicState.ContainsKey(key)) throw new AggregateException("重复插入");
        _dicState[key] = status;
    }
}