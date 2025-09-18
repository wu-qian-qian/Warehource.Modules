using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Domain.Device;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.DataBase.WcsTask.UpdateTask;

internal class UpdateWcsTaskCommandHandler(IWcsTaskRepository _wcsTaskRepositoty,IDeviceRepository _deviceRepository
    ,IUnitOfWork _unitofWork,IAnalysisLocation _locationService)
    : ICommandHandler<UpdateWcsTaskCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateWcsTaskCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<string>();
        var wcstask = _wcsTaskRepositoty.Get(request.SerialNumber);

        if (wcstask != null)
        {
            if (request.WcsTaskStatusType != null)
            {
                wcstask.TaskStatus = request.WcsTaskStatusType.Value;
            }
            if (request.DeviceName != null)
            {
                var device =  _deviceRepository.Get(request.DeviceName);
                if (device != null)
                {
                    wcstask.TaskExecuteStep.CurentDevice = device.DeviceName;
                    wcstask.TaskExecuteStep.DeviceType=device.DeviceType;
                }
            }
            if(request.PutLocation != null)
            {
                var location=_locationService.AnalysisPutLocation(request.PutLocation);
                wcstask.PutLocation = location;
            }
            if(request.GetLocation != null)
            {
                var location = _locationService.AnalysisGetLocation(request.GetLocation);
                wcstask.GetLocation= location;
            }
            if (request.Level.HasValue)
            {
                wcstask.Level = request.Level.Value;
            }
            if (request.IsEnforce.HasValue)
            {
                wcstask.IsEnforce = request.IsEnforce.Value;
            }
            await _unitofWork.SaveChangesAsync(cancellationToken);
        }
        else
        {
            result.SetMessage("无任务");
        }

        return result;
    }
}