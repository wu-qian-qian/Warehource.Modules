namespace User.Domain;

public class UserManager(IUserRepository userRepository, IRoleRepository roleRepository)
{
    public async Task<User?> GetUserAsync(string userName)
    {
        return (await userRepository.GetQueryableAsync()).FirstOrDefault(p => p.Username == userName);
    }

    public async Task<Role?> GetRoleAsync(string roleName)
    {
        return (await roleRepository.GetQueryableAsync()).FirstOrDefault(p => p.RoleName == roleName);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await userRepository.GetListAsync();
    }

    public async Task<IEnumerable<Role>> GetRolesAsync()
    {
        return await roleRepository.GetListAsync();
    }

    public async Task InserUserAsync(User user)
    {
        await userRepository.InserAsync(user);
    }

    public async Task InserRoleAsync(Role role)
    {
        await roleRepository.InserAsync(role);
    }

    public async Task<User?> GetUserAndRoleAsync(string userName)
    {
        return await userRepository.GetUserAndRoleAsync(userName);
    }

    public async Task<IEnumerable<User>> GetAllUserAndRoleAsync()
    {
        return await userRepository.GetAllUserAndRoleAsync();
    }
}