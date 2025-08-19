using Common.Application.MediatR.Message;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.Http.ApplyTunnle;

public class ApplyTunnleCommand : ICommand
{
    public WcsTask WcsTask { get; set; }
}