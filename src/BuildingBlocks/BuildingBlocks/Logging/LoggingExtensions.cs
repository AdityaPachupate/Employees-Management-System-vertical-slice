using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Logging;

public static class LoggingExtensions
{
    public static IServiceCollection AddCentralLogging(this IServiceCollection services, string serviceName, string loggingApiUrl)
    {
        services.AddHttpContextAccessor();

        services.AddHttpClient<ILogSender, CentralLogSender>(client =>
        {
            client.BaseAddress = new Uri(loggingApiUrl);
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            // For development purposes, allow untrusted certificates
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        });

        // The CentralLogSender needs to know its serviceName, so we override the factory
        services.AddTransient<ILogSender>(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient(nameof(ILogSender));
            httpClient.BaseAddress = new Uri(loggingApiUrl);

            var httpContextAccessor = sp.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            return new CentralLogSender(httpClient, httpContextAccessor, serviceName, sp);
        });

        return services;
    }
}
