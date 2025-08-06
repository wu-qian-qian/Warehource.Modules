using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Domain.ExecuteNode;

namespace Wcs.Application.DBHandler.ExecueNode.Delete;

public class DeleteExecuteNodeHandler(IExecuteNodeRepository _executeNodeRepository, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteExecuteNodeEvent, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteExecuteNodeEvent request, CancellationToken cancellationToken)
    {
        var result = new Result<string>();
        var entity = _executeNodeRepository.Get(request.Id);
        if (entity == null)
        {
            result.SetMessage("不存在节点");
        }
        else
        {
            result.SetValue("删除成功");
            await _unitOfWork.SaveChangesAsync();
        }

        return result;
    }
}