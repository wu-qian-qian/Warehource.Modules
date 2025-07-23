using System.Net;

namespace Common.Http;

public static class HttpHelper
{
    /// <summary>
    ///     保存文件
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static async Task SaveToFileAsync(this HttpResponseMessage httpRequest, string path,
        CancellationToken cancellationToken = default)
    {
        if (httpRequest.IsSuccessStatusCode == false)
            throw new ArgumentException($"状态码移除{nameof(httpRequest)}");
        using var info = new FileStream(path, FileMode.CreateNew);
        await httpRequest.Content.CopyToAsync(info, cancellationToken);
    }

    /// <summary>
    ///     下载文件
    /// </summary>
    /// <param name="client"></param>
    /// <param name="uri"></param>
    /// <param name="localPath"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<HttpStatusCode> DownlodaFileAsync(this HttpClient client, Uri uri, string localPath,
        CancellationToken cancellationToken = default)
    {
        var rep = await client.GetAsync(uri, cancellationToken);
        if (rep.IsSuccessStatusCode)
        {
            await SaveToFileAsync(rep, localPath, cancellationToken);
            return rep.StatusCode;
        }

        return HttpStatusCode.OK;
    }
}