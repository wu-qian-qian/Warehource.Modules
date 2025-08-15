using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.DataBase.Device.Delete;

public class DeleteDeviceCommand : ICommand<Result<string>>
{
    public Guid Id { get; set; }
}