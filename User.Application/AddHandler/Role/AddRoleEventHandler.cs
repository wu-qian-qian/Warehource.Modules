using AutoMapper;
using Common.Application.Exception;
using Common.Application.MediatR.Message;
using Identity.Application.Abstract;
using Identity.Contrancts;
using Identity.Domain;

namespace Identity.Application.AddHandler.Role;

internal class AddRoleEventHandler(IUnitOfWork unitOfWork, UserManager userManager, IMapper mapper)
    : ICommandHandler<AddRoleEvent, RoleDto>
{
    public async Task<RoleDto> Handle(AddRoleEvent request, CancellationToken cancellationToken)
    {
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
            return mapper.Map<RoleDto>(role);
        }

        throw new CommonException("添加角色失败存在该角色");
    }
}