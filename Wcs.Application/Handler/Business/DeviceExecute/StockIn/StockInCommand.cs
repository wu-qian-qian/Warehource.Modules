using Common.Application.MediatR.Message;
using Wcs.Device.Device.StockPort;

namespace Wcs.Application.Handler.Business.DeviceExecute.StockIn;

public class StockInCommand : ICommand
{
    public AbstractStockPort StockIn { get; set; }
}