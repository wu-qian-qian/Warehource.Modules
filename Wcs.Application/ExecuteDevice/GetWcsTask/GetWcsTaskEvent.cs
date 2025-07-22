using Common.Application.MediatR.Message;
using Wcs.Contracts.WcsTask;

namespace Wcs.Application.GetWcsTask;

public class GetWcsTaskEvent : ICommand<WcsTaskDto>
{
}