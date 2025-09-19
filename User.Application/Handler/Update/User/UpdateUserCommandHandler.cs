using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Identity.Application.Abstract;
using Identity.Contrancts.Respon;
using Identity.Domain;

namespace Identity.Application.Handler.Update.User;

internal class UpdateUserCommandHandler(IUnitOfWork unitOfWork, UserManager userManager, IMapper mapper)
    : ICommandHandler<UpdateUserCommand, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        Result<UserDto> result = new();
        var user = await userManager.GetUserAsync(request.Username);
        var role = await userManager.GetRoleAsync(request.RoleName);
        if (user != null && role != null)
        {
            user.Description = request.Description;
            user.Email = request.Email;
            user.Phone = request.Phone;
            if (request.LockoutEnd) user.LockoutEnd = DateTimeOffset.Now.AddDays(3);
            user.Username = request.Username;
            user.Password = request.Password;
            user.Name = request.Name;
            user.RoleId = role.Id;
            await userManager.UpdateUser(user);
            await unitOfWork.SaveChangesAsync();
            result.SetValue(mapper.Map<UserDto>(user));
        }
        else
        {
            result.SetMessage("角色信息错误");
        }

        return result;
    }
}