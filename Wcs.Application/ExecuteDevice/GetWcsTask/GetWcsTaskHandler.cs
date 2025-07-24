using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Application.ExecuteDevice.GetWcsTask;

/// <summary>
///     用来获取任务
/// </summary>
internal class GetWcsTaskHandler : ICommandHandler<GetWcsTaskEvent, WcsTaskDto>
{
    public Task<WcsTaskDto> Handle(GetWcsTaskEvent request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}