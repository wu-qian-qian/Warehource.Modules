using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.Http.Complate;

public class ComplateCommandEvent : ICommand<Result<string>>
{
}