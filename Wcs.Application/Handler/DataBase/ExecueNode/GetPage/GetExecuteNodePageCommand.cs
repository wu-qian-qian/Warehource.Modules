using Common.Application.MediatR.Message.PageQuery;
using Wcs.Contracts.Respon.ExecuteNode;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.ExecueNode.GetPage;

public class GetExecuteNodePageCommand : PageQuery<ExecuteNodeDto>
{
    /// <summary>
    ///     路径组
    /// </summary>
    public string? PahtNodeGroup { get; set; }

    /// <summary>
    ///     任务类型
    /// </summary>
    public WcsTaskTypeEnum? TaskType { get; set; }
}