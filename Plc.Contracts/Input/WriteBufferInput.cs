namespace Plc.Contracts.Input;

public class WriteBufferInput
{
    public string Ip { get; set; }

    public string Device { get; set; }

    public WriteBufferItemInput[] WriteBufferItemArray { get; set; }
}