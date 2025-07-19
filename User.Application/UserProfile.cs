using AutoMapper;
using User.Contrancts;

namespace User.Application;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Domain.User, UserDto>()
            .ForMember(dest => dest.RoleName,
                opt => opt.MapFrom(src => src.Role.RoleName));
    }
}