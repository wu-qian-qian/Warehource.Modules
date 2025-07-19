using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Database;
using Wcs.Infrastructure.Database;

namespace Warehource.Source.Database;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        ApplyMigration<WCSDBContext>(scope);
        ApplyMigration<UserDBContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using var context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        try
        {
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}