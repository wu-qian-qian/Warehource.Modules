namespace Wcs.Contracts.Respon.Plc;

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