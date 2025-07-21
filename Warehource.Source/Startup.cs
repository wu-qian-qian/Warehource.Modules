using Warehource.Source.Database;
using Wcs.Infrastructure;

namespace Warehource.Source;

public static class Startup
{
    public static IApplicationBuilder Initialization(this IApplicationBuilder app)
    {
        app.ApplyMigrations(); // 应用数据库迁移
        app.ApplicationServices.LoadJob();
        return app;
    }
}