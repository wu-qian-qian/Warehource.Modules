using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.Http.Complate;

public class ComplateCommand : ICommand<Result<string>>
{
}