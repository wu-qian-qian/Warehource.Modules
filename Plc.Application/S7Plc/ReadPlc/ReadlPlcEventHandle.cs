using Common.Application.MediatR.Message;
using Plc.Application.Abstract;

namespace Plc.Application.S7Plc.ReadPlc;

/// <summary>
///     最终处理者
/// </summary>
/// <param name="service"></param>
internal class ReadlPlcEventHandle(INetService service) : ICommandHandler<ReadPlcEvent, byte[]>
{
    /// <summary>
    ///     进行一些操作
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<byte[]> Handle(ReadPlcEvent request, CancellationToken cancellationToken)
    {
        if (request is ReadPlcEvent readCommand)
        {
        }

        return service.ReadAsync();
    }
}