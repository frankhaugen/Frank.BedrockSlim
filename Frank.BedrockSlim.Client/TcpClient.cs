using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Frank.BedrockSlim.Client;

public class TcpClient : ITcpClient
{
    private readonly ILogger<TcpClient> _logger;
    private readonly IOptions<TcpClientOptions> _options;

    public TcpClient(ILogger<TcpClient> logger, IOptions<TcpClientOptions> options)
    {
        _logger = logger;
        _options = options;
    }
    
    public async Task<ReadOnlyMemory<byte>> SendAsync(IPAddress serverIp, int serverPort, ReadOnlyMemory<byte> data)
    {
        var response = Memory<byte>.Empty;
        
        try
        {
            using var client = new System.Net.Sockets.TcpClient();
            await client.ConnectAsync(serverIp, serverPort);
            _logger.LogDebug("Connected to the server");
            
            await using var networkStream = client.GetStream();
            await networkStream.WriteAsync(data);

            if (networkStream.CanRead)
            {
                var buffer = new byte[4096];
                var cts = new CancellationTokenSource(_options.Value.Timeout);
                var task = networkStream.ReadAsync(buffer, 0, buffer.Length, cts.Token);
                
                try
                {
                    var bytesRead = await task;
                    response = buffer.AsMemory()[..bytesRead];
                }
                catch (OperationCanceledException)
                {
                    _logger.LogWarning("Read timed out");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while sending message");
        }

        return response;
    }
}