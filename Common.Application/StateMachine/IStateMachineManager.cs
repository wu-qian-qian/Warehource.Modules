namespace Common.Application.StateMachine;

public interface IStateMachineManager
{
    ValueTask NextStatus(string key, string json, CancellationToken token = default);

    void AddStates(string key, Type status);
}