using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.DB.ExecueNode.Delete;

public class DeleteExecuteNodeCommand : ICommand<Result<string>>
{
    public Guid Id { get; set; }
}