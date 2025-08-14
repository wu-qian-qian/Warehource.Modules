using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.DB.WcsTask.Delete;

public class DeleteWcsTaskCommandHandler(
    IWcsTaskRepository _wcsTaskRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper)
    : ICommandHandler<DeleteWcsTaskCommand, Result<WcsTaskDto>>
{
    public async Task<Result<WcsTaskDto>> Handle(DeleteWcsTaskCommand request, CancellationToken cancellationToken)
    {
        Result<WcsTaskDto> result = new();
        Domain.Task.WcsTask wcsTask = default;
        if (request.TaskCode != null)
            wcsTask = _wcsTaskRepository.Get(request.TaskCode);
        else
            wcsTask = _wcsTaskRepository.Get(request.SerialNumber.Value);
        if (wcsTask != null)
        {
            wcsTask.SoftDelete();
            await _unitOfWork.SaveChangesAsync();
            result.SetValue(_mapper.Map<WcsTaskDto>(wcsTask));
        }

        return result;
    }
}