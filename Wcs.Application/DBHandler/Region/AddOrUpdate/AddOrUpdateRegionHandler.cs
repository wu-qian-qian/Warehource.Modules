using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Region;
using Wcs.Domain.Region;

namespace Wcs.Application.DBHandler.Region.AddOrUpdate;

public class AddOrUpdateRegionHandler(
    IRegionRepository _regionPepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork) : ICommandHandler<AddOrUpdateRegionEvent, RegionDto>
{
    public async Task<RegionDto> Handle(AddOrUpdateRegionEvent request, CancellationToken cancellationToken)
    {
        Domain.Region.Region region = default;
        if (request.Id != null)
        {
            region = _regionPepository.Get(request.Id.Value);
            region.Code = request.Code;
            region.Description = request.Description;
        }
        else
        {
            region = new Domain.Region.Region();
            region.Code = request.Code;
            region.Description = request.Description;
            _regionPepository.Insert(region);
        }

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RegionDto>(region);
    }
}