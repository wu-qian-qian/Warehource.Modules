using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Identity.Application.Abstract;
using Identity.Contrancts;
using Identity.Domain;

namespace Identity.Application.Handler.Add.Role;

internal class AddRoleEventHandler(IUnitOfWork unitOfWork, UserManager userManager, IMapper mapper)
    : ICommandHandler<AddRoleEvent, Result<RoleDto>>
{
    public async Task<Result<RoleDto>> Handle(AddRoleEvent request, CancellationToken cancellationToken)
    {
        Result<RoleDto> result = new();
        var role = await userManager.GetRoleAsync(request.RoleName);
        if (role == null)
        {
            role = new Domain.Role
            {
                RoleName = request.RoleName,
                Description = request.Description
            };
            await userManager.InserRoleAsync(role);
            await unitOfWork.SaveChangesAsync();
            result.SetValue(mapper.Map<RoleDto>(role));
        }
        else
        {
            result.SetMessage("添加失败，无角色");
        }

        return result;
    }
}