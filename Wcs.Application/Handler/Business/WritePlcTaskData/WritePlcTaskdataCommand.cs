using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.Business.WritePlcTaskData;

public class WritePlcTaskdataCommand : ICommand
{
    public string Key { get; set; }
    public bool IsSucess { get; set; }
}