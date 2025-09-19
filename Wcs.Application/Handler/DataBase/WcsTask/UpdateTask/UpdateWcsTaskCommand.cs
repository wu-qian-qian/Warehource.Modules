using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.WcsTask.UpdateTask;

public class UpdateWcsTaskCommand : ICommand<Result<string>>
{
    public bool? IsEnforce { get; set; }
    public int? Level { get; set; }
    public int SerialNumber { get; set; }

    public string? DeviceName { get; set; }

    public WcsTaskStatusEnum? WcsTaskStatusType { get; set; }

    public string? GetLocation { get; set; }

    public string? PutLocation { get; set; }
}