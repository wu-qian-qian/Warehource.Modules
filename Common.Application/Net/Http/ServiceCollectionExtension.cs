using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Common.Application.Net.Http;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddHttpClient(this IServiceCollection services, HttpOptions options)
    {
        services.AddHttpClient();

        var httpClientBuilder = services.AddHttpClient(options.Name, client =>
        {
            client.BaseAddress = new Uri(options.BaseAddress);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Accept", "text/plain");
            client.Timeout = TimeSpan.FromMilliseconds(options.Timeout);
            if (options.GetAuthorization != null)
            {
                var httpResponse = client.Send(options.GetAuthorization);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var str = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + str);
                }
            }
        });

        // 是否启动重试
        if (options.EnablePolicy)
        {
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                // Handle HTTP 5xx errors or HTTP 408 requests  状态码这几个重试
                .WaitAndRetryAsync(
                    options.RetryCount, // 重试次数
                    attempt => TimeSpan.FromSeconds(options.RetryDelay), // 重试间隔时间
                    (outcome, timespan, retryAttempt, context) => { options.PolicCallback?.Invoke(outcome.Result); });
            httpClientBuilder.AddPolicyHandler(retryPolicy);
        }

        services.AddSingleton<HttpClientFactory>(sp =>
        {
            var basehttpFactory = sp.GetService<IHttpClientFactory>();
            var clientFactory = new HttpClientFactory(options.Name, basehttpFactory);
            return clientFactory;
        });
        return services;
    }
}