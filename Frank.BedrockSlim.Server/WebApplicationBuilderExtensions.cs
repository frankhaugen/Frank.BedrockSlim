using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.BedrockSlim.Server;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder UseTcpConnectionHandler<TProcessor>(this WebApplicationBuilder builder, int port)
        where TProcessor : class, IConnectionProcessor
    {
        // Add services
        builder.Services.AddTransient<IConnectionProcessor, TProcessor>();

        // Configure Kestrel
        builder.WebHost.UseKestrelCore();
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.ListenAnyIP(port, listenOptions =>
            {
                listenOptions.UseConnectionHandler<TcpConnectionHandler>();
            });
        });

        return builder;
    }
}