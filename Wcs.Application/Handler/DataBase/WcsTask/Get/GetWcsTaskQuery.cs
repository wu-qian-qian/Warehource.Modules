using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.WcsTask.Get;

public class GetWcsTaskQuery : IQuery<IEnumerable<WcsTaskDto>>
{
    /// <summary>
    ///     任务状态
    /// </summary>
    public CreatorSystemTypeEnum? CreatorSystemType { get; set; }

    public string? Container { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? TaskCode { get; set; }

    public int? SerialNumber { get; set; }

    public string? GetLocation { get; set; }

    public string? PutLocation { get; set; }
}