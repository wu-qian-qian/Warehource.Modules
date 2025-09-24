using Common.Shared;
using Common.Shared.Log;
using Serilog;

namespace Common.Application.Log;

public static class LoggerExtensions
{
    public static ILogger ForCategory(this ILogger logger, LogCategory category)
    {
        return logger.ForContext("Category", category);
    }

    public static void HttpInformation(this ILogger logger, LogCategory category, HttpLog log)
    {
        var request = log.Request
            .Replace("\r", "") // ȥ���س���
            .Replace("\n", ""); // ȥ�����з�;

        var respon = log.Respon
            .Replace("\r", "") // ȥ���س���
            .Replace("\n", ""); // ȥ�����з�

        logger.ForContext("Category", category).Information(
            "URL: {URL}, TimeUsed: {TimeUsed}, Request: {Request}, Response: {Response}"
            , log.URL, log.TimeUsed, request, respon);
    }

    public static void BusinessInformation(this ILogger logger, LogCategory category, BusinessLog log)
    {
        logger.ForContext("Category", category).Information(
            "DeviceName: {DeviceName}, SerialNum: {SerialNum}, Message: {Message}"
            , log.DeviceName, log.SerialNum, log.Message);
    }
}