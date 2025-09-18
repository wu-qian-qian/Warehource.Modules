using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Identity.Application.Abstract;
using Identity.Contrancts.Respon;
using Identity.Domain;

namespace Identity.Application.Handler.Update.Role;

internal class UpdateRoleEventCommandHandler(IUnitOfWork unitOfWork, UserManager userManager, IMapper mapper)
    : ICommandHandler<UpdateRoleCommand, Result<RoleDto>>
{
    public async Task<Result<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Result<RoleDto> result = new();
        var role = await userManager.GetRoleAsync(request.RoleName);
        if (role != null)
        {
            role.Description = request.Description;
            role.RoleName = request.RoleName;
            await userManager.UpdateRole(role);
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