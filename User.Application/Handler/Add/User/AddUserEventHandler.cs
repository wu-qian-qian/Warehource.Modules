using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Identity.Application.Abstract;
using Identity.Contrancts;
using Identity.Domain;

namespace Identity.Application.Handler.Add.User;

internal class AddUserEventHandler(IUnitOfWork unitOfWork, UserManager userManager, IMapper mapper)
    : ICommandHandler<AddUserEvent, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(AddUserEvent request, CancellationToken cancellationToken)
    {
        Result<UserDto> result = new();
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
            result.SetValue(mapper.Map<UserDto>(user));
        }

        else
        {
            result.SetMessage("角色信息错误");
        }

        return result;
    }
}