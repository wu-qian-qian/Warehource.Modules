using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Application.Handler.DataBase.WcsTask.GetData;

public class GetDataCommand : ICommand<IEnumerable<GetWcsDataDto>>
{
    public DateTimeOffset StartTime { get; set; }

    public DateTimeOffset EndTime { get; set; }
}