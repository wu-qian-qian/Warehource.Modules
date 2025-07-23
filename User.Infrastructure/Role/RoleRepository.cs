using Common.Infrastructure.EF.Repository;
using Identity.Domain;
using Identity.Infrastructure.Database;

namespace Identity.Infrastructure.Role;

internal class RoleRepository : EfCoreRepository<Domain.Role, UserDBContext>, IRoleRepository
{
    public RoleRepository(UserDBContext dbContext) : base(dbContext)
    {
    }
}