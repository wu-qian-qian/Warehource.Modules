using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Region;

namespace Wcs.Application.DBHandler.Region.Delete;

public class DeleteRegionEvent : ICommand<RegionDto>
{
    public Guid? Id { get; set; }
}