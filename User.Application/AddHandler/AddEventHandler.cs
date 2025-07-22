using AutoMapper;
using Common.Application.Exception;
using Common.Application.MediatR.Message;
using User.Application.Abstract;
using User.Contrancts;
using User.Domain;

namespace User.Application.AddHandler;

internal class AddUserEventHandler(IUnitOfWork unitOfWork, UserManager userManager, IMapper mapper)
    : ICommandHandler<AddUserEvent, UserDto>
{
    public async Task<UserDto> Handle(AddUserEvent request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserAsync(request.Username);
        var role = await userManager.GetRoleAsync(request.RoleName);
        var LockoutEnd = DateTimeOffset.Now.AddYears(-100);
        if (user == null && role != null)
        {
            user = new Domain.User
            {
                Description = request.Description,
                Email = request.Email,
                Phone = request.Phone,
                LockoutEnd = LockoutEnd,
                Username = request.Username,
                Password = request.Password,
                Name = request.Name,
                RoleId = role.Id
            };
            await userManager.InserUserAsync(user);
            await unitOfWork.SaveChangesAsync();
            user.Role = role;
            return mapper.Map<UserDto>(user);
        }

        throw new CommonException("添加角色失败存在该用户");
    }
}

internal class AddRoleEventHandler(IUnitOfWork unitOfWork, UserManager userManager, IMapper mapper)
    : ICommandHandler<AddRoleEvent, RoleDto>
{
    public async Task<RoleDto> Handle(AddRoleEvent request, CancellationToken cancellationToken)
    {
        var role = await userManager.GetRoleAsync(request.RoleName);
        if (role == null)
        {
            role = new Role
            {
                RoleName = request.RoleName,
                Description = request.Description
            };
            await userManager.InserRoleAsync(role);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<RoleDto>(role);
        }

        throw new CommonException("添加角色失败存在该角色");
    }
}