namespace Plc.Contracts.DataModel;

public struct ReadModel
{
    public ReadModel(string dbName, string data)
    {
        DBName = dbName;

        Data = data;
    }

    public string DBName { get; set; }

    public string Data { get; set; }
}