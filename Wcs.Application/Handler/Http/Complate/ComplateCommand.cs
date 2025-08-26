using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.Http.Complate;

public class ComplateCommand : ICommand<Result<string>>
{
    public WcsTask WcsTask { get; set; }
}