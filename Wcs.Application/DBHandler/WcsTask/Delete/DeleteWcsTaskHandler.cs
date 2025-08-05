using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Domain.Task;

namespace Wcs.Application.DBHandler.WcsTask.Cancel;

public class DeleteWcsTaskHandler(IWcsTaskRepository _wcsTaskRepository
        ,IUnitOfWork _unitOfWork,IMapper _mapper):ICommandHandler<DeleteWcsTaskEvent,WcsTaskDto>
{
    public async Task<WcsTaskDto> Handle(DeleteWcsTaskEvent request, CancellationToken cancellationToken)
    {
        Domain.Task.WcsTask wcsTask = default;
        if (request.TaskCode != null)
        {
            wcsTask = _wcsTaskRepository.Get(request.TaskCode);
        }
        else
        {
            wcsTask = _wcsTaskRepository.Get(request.SerialNumber.Value);
        }
        wcsTask.SoftDelete();
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<WcsTaskDto>(wcsTask);
    }
}