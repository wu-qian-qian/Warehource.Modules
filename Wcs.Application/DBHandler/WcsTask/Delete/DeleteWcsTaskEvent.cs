using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Application.DBHandler.WcsTask.Cancel;

public class DeleteWcsTaskEvent:ICommand<WcsTaskDto>
{
    public int? SerialNumber { get; set; }
    
    public string? TaskCode { get; set; }
}