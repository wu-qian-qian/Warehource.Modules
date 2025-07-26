using Plc.Shared;

namespace Plc.Contracts.Input;

public class ReadBufferInput
{
    public string Ip { get; set; }
    
    public int DBAddress { get; set; }
    
    public int DBStart { get; set; }
    
    public int DBEnd { get; set; }
    
    public S7BlockTypeEnum S7BlockType { get; set; }

    public int HashId { get; set; }
}