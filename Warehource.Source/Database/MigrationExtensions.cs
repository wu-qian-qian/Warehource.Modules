using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Plc.Infrastructure.Database;
using Wcs.Infrastructure.Database;

namespace Warehource.Source.Database;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        ApplyMigration<WCSDBContext>(scope);
        ApplyMigration<UserDBContext>(scope);
        ApplyMigration<PlcDBContext>(scope);
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