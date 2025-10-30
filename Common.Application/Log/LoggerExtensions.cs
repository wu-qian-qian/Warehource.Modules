using Common.Shared;
using Common.Shared.Log;
using Serilog;
using System.Collections.Concurrent;

namespace Common.Application.Log;

public static class LoggerExtensions
{
    /// <summary>
    /// 防止缓存的重复记录
    /// </summary>
    public static ConcurrentDictionary<string, DateTime> LastLogTimes = new();

    static LoggerExtensions()
    {
        Thread thread = new(() =>
        {
            while (true)
            {
                var now = DateTime.Now;
                var keysToRemove = LastLogTimes
                    .Where(kvp => (now - kvp.Value).TotalSeconds > 15)
                    .Select(kvp => kvp.Key)
                    .ToList();
                foreach (var key in keysToRemove)
                {
                    LastLogTimes.TryRemove(key, out _);
                }

                Thread.Sleep(20 * 1000); // 每20秒检查一次
            }
        });
        thread.Start();
    }

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

    //public static void BusinessInformation(this ILogger logger, LogCategory category, BusinessLog log)
    //{

    //    logger.ForContext("Category", category).Information(
    //   "DeviceName: {DeviceName}, SerialNum: {SerialNum}, Message: {Message}"
    //   , log.DeviceName, log.SerialNum, log.Message);
    //}

    /// <summary>
    /// 业务日志，防止time秒内重复日志记录
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="category"></param>
    /// <param name="log"></param>
    /// <param name="time"></param>
    public static void BusinessInformation(this ILogger logger, LogCategory category, BusinessLog log, int time = 10)
    {
        var key = log.ToString().GetHashCode().ToString();
        if (LastLogTimes.TryGetValue(key, out var lastTime))
        {
            if (lastTime.AddSeconds(time) > DateTime.Now)
            {
                // 5秒内重复日志不记录
                return;
            }
            else
            {
                LastLogTimes[key] = DateTime.Now;
            }
        }

        logger.ForContext("Category", category).Information(
            "DeviceName: {DeviceName}, SerialNum: {SerialNum}, Message: {Message}"
            , log.DeviceName, log.SerialNum, log.Message);
    }
}