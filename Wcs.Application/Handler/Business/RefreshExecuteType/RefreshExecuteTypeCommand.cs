using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.Business.RefreshExecuteType;

public class RefreshExecuteTypeCommand : ICommand
{
    public string Key { get; set; }
}