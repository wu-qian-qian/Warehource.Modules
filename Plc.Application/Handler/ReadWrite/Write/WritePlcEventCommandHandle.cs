using Common.Application.MediatR.Message;
using Plc.Application.Abstract;
using Plc.Domain.S7;

namespace Plc.Application.Handler.ReadWrite.Write;

/// <summary>
///     最终处理者
/// </summary>
/// <param name="service"></param>
internal class WritePlcEventCommandHandle(INetService netService, IS7NetManager netManager)
    : ICommandHandler<WritePlcEventCommand, bool>
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(WritePlcEventCommand request, CancellationToken cancellationToken)
    {
        var @bool = false;
        foreach (var item in request.writeBufferInputs)
        {
            @bool = await netService.CheckWriteByteAsync(item);
            if (@bool == false)
                break;
        }

        return @bool;
    }
}