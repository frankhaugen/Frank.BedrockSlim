using Frank.BedrockSlim.Cryptography;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.BedrockSlim.Server;

public static class WebHostBuilderExtensions
{
    public static IWebHostBuilder UseTcpConnectionHandler<THandler>(this IWebHostBuilder builder, int port)
        where THandler : class, IConnectionHandler
    {
        builder.ConfigureServices(services =>
        {
            services.AddTransient<IConnectionHandler, THandler>();
            services.AddAdvancedEncryption();
        });

        builder.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(port, listenOptions =>
            {
                listenOptions.UseConnectionHandler<TcpConnectionHandler>();
            });
        });

        return builder;
    }
}