using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MediatR;

namespace Common.Application.MediatR.Behaviors;

/// <summary>
///     用于执行前取消令牌的检查
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public sealed class CancelPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            return next();
        }
        catch (OperationCanceledException ex)
        {
            Serilog.Log.Logger.ForCategory(LogCategory.Error).Information("任务超时触发结束");
            return Task.FromResult(default(TResponse));
        }
    }
}