using Common.Shared;
using Serilog;

namespace Common.Application.Log;

public static class LoggerExtensions
{
    public static ILogger ForCategory(this ILogger logger, LogCategory category)
    {
        return logger.ForContext("Category", category);
    }
}