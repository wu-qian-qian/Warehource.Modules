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
        var user = await userManager.GetUserAsync(request.Name);
        var role = await userManager.GetRoleAsync(request.RoleName);
        var LockoutEnd = DateTimeOffset.Now.AddYears(-100);
        if (user == null && role != null)
        {
            user = new Domain.User
            {
                RoleId = role.Id,
                Description = request.Description,
                Email = request.Email,
                Phone = request.Phone,
                LockoutEnd = LockoutEnd,
                Username = request.Name,
                Password = request.Password,
                Name = request.Name
            };
            await userManager.InserUserAsync(user);
            return mapper.Map<UserDto>(user);
        }

        throw new CommonException("添加角色失败存在该用户");
    }
}

internal class AddRoleEventHandler(IUnitOfWork unitOfWork, UserManager userManager)
    : ICommandHandler<AddRoleEvent, RoleDto>
{
    public Task<RoleDto> Handle(AddRoleEvent request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}