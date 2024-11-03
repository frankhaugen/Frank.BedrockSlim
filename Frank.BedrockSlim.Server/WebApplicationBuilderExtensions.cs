using Frank.BedrockSlim.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.BedrockSlim.Server;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder UseTcpConnectionHandler<THandler>(this WebApplicationBuilder builder, int port)
        where THandler : class, IConnectionHandler
    {
        // Add services
        builder.Services.AddTransient<IConnectionHandler, THandler>();
        builder.Services.AddAdvancedEncryption();

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