using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Region;
using Wcs.Domain.Region;

namespace Wcs.Application.Handler.DB.Region.Delete;

public class DeleteRegionCommandHandler(IRegionRepository _regionRepository, IMapper _mapper, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteRegionCommand, Result<RegionDto>>
{
    public async Task<Result<RegionDto>> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
    {
        Result<RegionDto> result = new();
        var region = _regionRepository.Get(request.Id.Value);
        region?.SoftDelete();
        await _unitOfWork.SaveChangesAsync();
        result.SetValue(_mapper.Map<RegionDto>(region));
        return result;
    }
}