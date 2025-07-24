using AutoMapper;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application;

internal class PlcProfile : Profile
{
    public PlcProfile()
    {
        CreateMap<S7NetConfig, S7NetDto>();
        CreateMap<S7EntityItem, S7EntityItemDto>();
    }
}