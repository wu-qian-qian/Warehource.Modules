using Common.Application.MediatR.Message;
using Wcs.Device.Device.StockPort;

namespace Wcs.Application.Handler.Business.DeviceExecute.StockOut;

public class StockOutCommand : ICommand
{
    public AbstractStockPort StockOut { get; set; }
}