using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.DB.PlcMap.InsertOrUpdate;

public class InsertOrUpdateCommand : ICommand
{
    public string DeviceName { get; set; }

    public string[] PlcEntityName { get; set; }

    public KeyValuePair<string, string>[]? UpdateList { get; set; }
}