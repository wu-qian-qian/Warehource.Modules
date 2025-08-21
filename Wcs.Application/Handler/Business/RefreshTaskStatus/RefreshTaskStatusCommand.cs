using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.Business.RefreshExecuteType;

public class RefreshTaskStatusCommand : ICommand
{
    public string Key { get; set; }
}