using MediatR;
using Plc.Application.S7Plc.ReadPlc;

namespace Plc.Application.S7Plc.Behaviors;

/// <summary>
///     ReadlPlcEventHandle 的中间管道，同来做一些解析
///     Sender 触发传入DeviceEventCommand  经过该层解析ReadPlcEventCommand
/// </summary>
internal class ReadS7PlcPipelineBehavior : IPipelineBehavior<PlcEventCommand, byte[]>
{
    public async Task<byte[]> Handle(PlcEventCommand request, RequestHandlerDelegate<byte[]> next,
        CancellationToken cancellationToken)
    {
        request = PlcAnalysis(request);
        return await next();
    }

    /// <summary>
    ///     将变量模型解析成PLC读取数据的模型
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    internal static ReadPlcEvent PlcAnalysis(PlcEventCommand request)
    {
        var readPlcEvent = new ReadPlcEvent();
        readPlcEvent.Ip = request.Ip;
        request = readPlcEvent;
        return readPlcEvent;
    }
}