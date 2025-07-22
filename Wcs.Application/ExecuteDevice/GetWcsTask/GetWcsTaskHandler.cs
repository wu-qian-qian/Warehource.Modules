using Common.Application.MediatR.Message;
using Wcs.Application.GetWcsTask;
using Wcs.Contracts.WcsTask;

namespace Wcs.Application.ExecuteDevice;

internal class GetWcsTaskHandler : ICommandHandler<GetWcsTaskEvent, WcsTaskDto>
{
    public Task<WcsTaskDto> Handle(GetWcsTaskEvent request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}