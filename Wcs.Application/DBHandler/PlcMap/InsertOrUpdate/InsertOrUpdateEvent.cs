using Common.Application.MediatR.Message;

namespace Wcs.Application.DBHandler.PlcMap.InsertOrUpdate;

public class InsertOrUpdateEvent : ICommand
{
    public string DeviceName { get; set; }

    public string[] PlcEntityName { get; set; }

    public KeyValuePair<string, string>[]? UpdateList { get; set; }
}