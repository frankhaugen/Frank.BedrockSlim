using Frank.BedrockSlim.Cryptography;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Frank.BedrockSlim.Server;

public static class TcpServerHostBuilderExtensions
{
    public static IHostBuilder UseTcpConnectionHandler<THandler>(this IHostBuilder hostBuilder, int port)
        where THandler : class, IConnectionHandler
    {
        hostBuilder.ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IConnectionHandler, THandler>();
                services.AddAdvancedEncryption();
            });

            webBuilder.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(port, listenOptions =>
                {
                    listenOptions.UseConnectionHandler<TcpConnectionHandler>();
                });
            });
        });

        return hostBuilder;
    }
}