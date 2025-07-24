using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Application.ExecuteDevice.GetWcsTask;

public class GetWcsTaskEvent : ICommand<WcsTaskDto>
{
}