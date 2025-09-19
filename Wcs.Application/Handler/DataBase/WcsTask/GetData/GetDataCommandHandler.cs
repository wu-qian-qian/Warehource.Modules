using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.WcsTask.GetData;

internal class GetDataCommandHandler(IWcsTaskRepository _wcsTaskRepository)
    : ICommandHandler<GetDataCommand, IEnumerable<GetWcsDataDto>>
{
    public Task<IEnumerable<GetWcsDataDto>> Handle(GetDataCommand request, CancellationToken cancellationToken)
    {
        //可考虑用缓存
        var query = _wcsTaskRepository.GetWcsTaskQuerys()
            .Where(p => p.CreationTime >= request.StartTime && p.CreationTime <= request.EndTime)
            .Where(p => p.TaskStatus != WcsTaskStatusEnum.Cancelled)
            .Select(p => new { p.CreationTime.Hour, p.TaskStatus }).OrderBy(p => p.Hour).ToArray();
        var group =
            query.GroupBy(p => p.Hour);
        var result = group.Select(p =>
        {
            var dto = new GetWcsDataDto
            {
                Name = $"{p.Key}点",
                Value = p.Count(p => p.TaskStatus == WcsTaskStatusEnum.Completed),
                Growth = p.Count(p => p.TaskStatus != WcsTaskStatusEnum.Completed)
            };
            return dto;
        });
        //result = result.OrderBy(p => p.Name).AsEnumerable();
        return Task.FromResult(result);
    }
}