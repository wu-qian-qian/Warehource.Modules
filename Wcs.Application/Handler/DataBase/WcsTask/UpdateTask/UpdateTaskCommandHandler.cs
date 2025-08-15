using System.Reflection;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.DataBase.WcsTask.UpdateTask;

public class UpdateTaskCommandHandler(IWcsTaskRepository _wcsTaskRepository, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdataTaskCommand>
{
    private static readonly IEnumerable<PropertyInfo> propertyInfos = typeof(Domain.Task.WcsTask).GetProperties();

    public async Task Handle(UpdataTaskCommand request, CancellationToken cancellationToken)
    {
        var wcsTask = _wcsTaskRepository.Get(request.SerialNumber);
        if (wcsTask != null)
        {
            foreach (var item in request.DataMap)
                propertyInfos.First(p => p.Name == item.Key).SetValue(wcsTask, item.Value);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}