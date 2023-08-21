using System.Reflection;
using Abstractions.Providers;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;

namespace Abstractions;

public static class Injector
{
    public static IServiceCollection AddProvider(this IServiceCollection services)
    {
        services.AddRefitClient<IMainClient>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:5022/"))
            .AddTransientHttpErrorPolicy(builder =>
                builder.WaitAndRetryAsync(5, retryCnt => TimeSpan.FromSeconds(Math.Pow(2, retryCnt)))
            );

        return services;
    }
}
