namespace Wcs.Contracts.Respon.Plc;

public struct PlcBuffer
{
    public PlcBuffer(string dbName, string data)
    {
        DBName = dbName;

        Data = data;
    }

    public string DBName { get; set; }

    public string Data { get; set; }
}