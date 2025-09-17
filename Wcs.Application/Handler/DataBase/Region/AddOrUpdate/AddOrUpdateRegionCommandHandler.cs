using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Region;
using Wcs.Domain.Region;

namespace Wcs.Application.Handler.DataBase.Region.AddOrUpdate;

public class AddOrUpdateRegionCommandHandler(
    IRegionRepository _regionPepository,
    IMapper _mapper,
    IUnitOfWork _unitOfWork) : ICommandHandler<AddOrUpdateRegionCommand, Result<RegionDto>>
{
    public async Task<Result<RegionDto>> Handle(AddOrUpdateRegionCommand request, CancellationToken cancellationToken)
    {
        Result<RegionDto> result = new();
        Domain.Region.Region region = default;
        if (request.Id != null)
        {
            region = _regionPepository.Get(request.Id.Value);
            region.Code = request.Code;
            region.Description = request.Description;
            region.MaxNum = request.MaxNum;
        }
        else
        {
            region = new Domain.Region.Region();
            region.Code = request.Code;
            region.Description = request.Description;
            _regionPepository.Insert(region);
        }

        await _unitOfWork.SaveChangesAsync();
        result.SetValue(_mapper.Map<RegionDto>(region));
        return result;
    }
}