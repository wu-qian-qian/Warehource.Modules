using System.Reflection;
using Common.Application.StateMachine;
using Common.Domain.State;
using Common.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.StateMachine;

public static class StateMachineExtension
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection AddStateMachineExtensionConfiguator(this IServiceCollection services,
        Assembly[] assemblies)
    {
        if (assemblies == null || assemblies.Length == 0)
            throw new ArgumentException("At least one assembly must be provided.", nameof(assemblies));
        var dictionary = new Dictionary<string, Type[]>();
        var assTypeList = assemblies.Select(p => p.GetTypes());
        var types = new List<Type>();
        foreach (var item in assTypeList) types.AddRange(item);

        var stateMachines = types.Where(type => type is { IsAbstract: false, IsInterface: false } &&
                                                type.IsAssignableTo(typeof(IStateMachine))).ToArray();
        foreach (var handler in stateMachines) services.AddTransient(handler);
        services.AddSingleton<IStateMachineManager>(sp =>
        {
            var serviceScope = sp.GetRequiredService<IServiceScopeFactory>();
            IStateMachineManager manager = new StateMachineManager(serviceScope);
            for (var i = 0; i < stateMachines.Length; i++)
            {
                var stateMachine = stateMachines[i];
                var att = stateMachine.GetCustomAttribute<StateAttrubite>();
                if (att == null) continue;
                manager.AddStates(att.KeyName, stateMachine);
            }

            return manager;
        });

        return services;
    }
}