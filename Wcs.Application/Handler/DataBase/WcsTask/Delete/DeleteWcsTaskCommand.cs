using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Application.Handler.DataBase.WcsTask.Delete;

public class DeleteWcsTaskCommand : ICommand<Result<WcsTaskDto>>
{
    public int? SerialNumber { get; set; }

    public string? TaskCode { get; set; }
}