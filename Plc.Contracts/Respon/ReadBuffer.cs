namespace Plc.Contracts.Respon;

public struct ReadBuffer
{
    public ReadBuffer(int db, byte[] data)
    {
        DB = db;

        Data = data;
    }

    public int DB { get; set; }

    public byte[] Data { get; set; }
}