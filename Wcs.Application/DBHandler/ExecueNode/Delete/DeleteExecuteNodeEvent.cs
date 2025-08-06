using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.DBHandler.ExecueNode.Delete;

public class DeleteExecuteNodeEvent : ICommand<Result<string>>
{
    public Guid Id { get; set; }
}