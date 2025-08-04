using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Region;
using Wcs.Domain.Region;

namespace Wcs.Application.DBHandler.Region.Delete;

public class DeleteRegionHandler(IRegionRepository _regionRepository, IMapper _mapper, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteRegionEvent, RegionDto>
{
    public async Task<RegionDto> Handle(DeleteRegionEvent request, CancellationToken cancellationToken)
    {
        var region = _regionRepository.Get(request.Id.Value);
        region?.SoftDelete();
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RegionDto>(region);
    }
}