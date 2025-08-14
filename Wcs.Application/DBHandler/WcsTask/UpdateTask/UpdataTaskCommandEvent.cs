using Common.Application.MediatR.Message;

namespace Wcs.Application.DBHandler.WcsTask.UpdateTask;

public class UpdataTaskCommandEvent : ICommand
{
    public int SerialNumber { get; set; }

    /// <summary>
    ///     属性   数据 的对应
    /// </summary>
    public IEnumerable<KeyValuePair<string, object>> DataMap { get; set; }
}