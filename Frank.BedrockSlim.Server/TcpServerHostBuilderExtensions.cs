using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Frank.BedrockSlim.Server;

public static class TcpServerHostBuilderExtensions
{
    public static IHostBuilder UseTcpConnectionHandler<TProcessor>(this IHostBuilder hostBuilder, int port)
        where TProcessor : class, IConnectionProcessor
    {
        hostBuilder.ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.ConfigureServices(services =>
            {
                services.AddTransient<IConnectionProcessor, TProcessor>();
            });

            webBuilder.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(port, listenOptions =>
                {
                    ConnectionBuilderExtensions.UseConnectionHandler<TcpConnectionHandler>(listenOptions);
                });
            });
        });

        return hostBuilder;
    }
}