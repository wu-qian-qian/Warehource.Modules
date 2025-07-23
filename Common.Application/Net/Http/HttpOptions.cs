namespace Common.Application.Net.Http;

public sealed record HttpOptions
{
    /// <summary>
    ///     每一次重试都调用
    /// </summary>
    public Action<HttpResponseMessage>? PolicCallback;

    public string BaseAddress { get; set; }

    public ushort Timeout { get; set; }

    public string Name { get; set; }

    public bool EnablePolicy { get; set; }

    /// <summary>
    ///     重试次数
    /// </summary>
    public int RetryCount { get; set; }

    /// <summary>
    ///     重试休眠事件间隔
    /// </summary>
    public int RetryDelay { get; set; }

    public HttpRequestMessage? GetAuthorization { get; set; }
}