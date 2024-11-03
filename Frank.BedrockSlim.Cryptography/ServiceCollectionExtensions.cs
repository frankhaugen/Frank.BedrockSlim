using Microsoft.Extensions.DependencyInjection;

namespace Frank.BedrockSlim.Cryptography;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdvancedEncryption(this IServiceCollection services, Action<AdvancedEncryptionOptions>? configureOptions = null)
    {
        var options = new AdvancedEncryptionOptions();
        configureOptions?.Invoke(options);

        services.AddSingleton(options);
        services.AddSingleton<IAdvancedEncryptionFactory, AdvancedEncryptionFactory>();
        services.AddSingleton<IAdvancedEncryptionService, AdvancedEncryptionService>();
        return services;
    }
}