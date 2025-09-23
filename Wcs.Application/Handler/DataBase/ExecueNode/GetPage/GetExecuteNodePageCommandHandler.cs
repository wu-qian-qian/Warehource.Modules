using AutoMapper;
using Common.Application.MediatR.Message.PageQuery;
using Common.Helper;
using Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Contracts.Respon.ExecuteNode;
using Wcs.Domain.ExecuteNode;

namespace Wcs.Application.Handler.DataBase.ExecueNode.GetPage
{
    internal class GetExecuteNodePageCommandHandler(IExecuteNodeRepository _executeNodeRepository, IMapper _mapper)
        : IPageHandler<GetExecuteNodePageCommand, ExecuteNodeDto>
    {
        public Task<PageResult<ExecuteNodeDto>> Handle(GetExecuteNodePageCommand request,
            CancellationToken cancellationToken)
        {
            var query = _executeNodeRepository.GetQuerys()
                .WhereIf(request.PahtNodeGroup != null, p => p.PahtNodeGroup == request.PahtNodeGroup)
                .WhereIf(request.TaskType != null, p => p.TaskType == request.TaskType);
            var count = query.Count();
            var data = query.ToPageBySortAsc(request.SkipCount, request.Total, p => p.CreationTime).ToArray();
            var list = _mapper.Map<List<ExecuteNodeDto>>(data);
            return Task.FromResult(new PageResult<ExecuteNodeDto>(count, list));
        }
    }
}