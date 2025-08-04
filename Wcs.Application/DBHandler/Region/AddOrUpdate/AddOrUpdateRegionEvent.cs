using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Region;

namespace Wcs.Application.DBHandler.Region.AddOrUpdate;

public class AddOrUpdateRegionEvent : ICommand<RegionDto>
{
    public string Code { get; set; }

    public string? Description { get; set; }

    public Guid? Id { get; set; }
}