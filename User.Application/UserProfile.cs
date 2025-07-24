using AutoMapper;
using Identity.Contrancts;
using Identity.Domain;

namespace Identity.Application;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.RoleName,
                opt => opt.MapFrom(src => src.Role.RoleName));
        CreateMap<Role, RoleDto>();
    }
}