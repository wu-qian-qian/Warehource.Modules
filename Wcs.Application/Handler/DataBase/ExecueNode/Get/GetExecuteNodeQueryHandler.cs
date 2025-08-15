using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Common.Helper;
using Wcs.Contracts.Respon.ExecuteNode;
using Wcs.Domain.ExecuteNode;

namespace Wcs.Application.Handler.DataBase.ExecueNode.Get;

public class GetExecuteNodeQueryHandler(IExecuteNodeRepository _executeNodeRepository, IMapper _mapper)
    : IQueryHandler<GetExecuteNodeQuery, Result<IEnumerable<ExecuteNodeDto>>>
{
    public Task<Result<IEnumerable<ExecuteNodeDto>>> Handle(GetExecuteNodeQuery request,
        CancellationToken cancellationToken)
    {
        var result = new Result<IEnumerable<ExecuteNodeDto>>();
        var data = _executeNodeRepository.GetQuerys()
            .WhereIf(request.PahtNodeGroup != null, x => x.PahtNodeGroup == request.PahtNodeGroup)
            .WhereIf(request.TaskType != null, x => x.TaskType == request.TaskType)
            .ToList();
        var source = _mapper.Map<IEnumerable<ExecuteNodeDto>>(data);
        result.SetValue(source);
        return Task.FromResult(result);
    }
}