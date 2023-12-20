using System.Net;

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
                _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
                await _tcpClient.SendAsync(IPAddress.Loopback, 6667, "Hello World"u8.ToArray());
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}