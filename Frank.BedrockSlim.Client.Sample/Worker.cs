using System.Net;
using System.Text;

namespace Frank.BedrockSlim.Client.Sample;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ITcpClient _tcpClient;

    public Worker(ILogger<Worker> logger, ITcpClient tcpClient)
    {
        _logger = logger;
        _tcpClient = tcpClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                var response = await _tcpClient.SendAsync(IPAddress.Loopback, 6667, "Hello World"u8.ToArray());
                
                if (response.Length > 0)
                {
                    _logger.LogInformation("Received: {Response}", Encoding.UTF8.GetString(response.ToArray()));
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}