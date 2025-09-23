using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using Plc.Application.Behaviors;
using Serilog;

namespace Plc.Application.Handler.ReadWrite.ReLoad;

internal class ReloadCommandHandler : ICommandHandler<ReloadCommand>
{
    public Task Handle(ReloadCommand request, CancellationToken cancellationToken)
    {
        var ex = PlcReadWriteDtoHelper.ReloadRead();
        if (ex == null)
            ex = PlcReadWriteDtoHelper.ReloadWrite();
        else
            Log.Logger.ForCategory(LogCategory.Net).Information("PLC缓存重新加载");

        return Task.CompletedTask;
    }
}