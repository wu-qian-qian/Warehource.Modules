using Common.Application.MediatR.Message;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.Business.RefreshTaskStatus;

public class RefreshTaskStatusCommand : ICommand
{
    public string Key { get; set; }

    public WcsTask WcsTask { get; set; }
}