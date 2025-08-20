namespace UI.Model.Device;

public class StackerConfig
{
    public int Tunnle { get; set; } = 1;
}

public class StckerTranShipConfig
{
    public string PipelinCode { get; set; } = string.Empty;

    public int Row { get; set; } = 1;

    public int Column { get; set; } = 1;

    public int Floor { get; set; } = 1;

    public int Tunnle { get; set; } = 1;
}

public class StockPortConfig
{
    public string PipelinCode { get; set; } = string.Empty;
}