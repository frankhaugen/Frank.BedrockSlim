using System.Net;
using System.Text;
using Frank.BedrockSlim.Cryptography;

namespace Frank.BedrockSlim.Client.Sample;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ITcpClient _tcpClient;
    private readonly IAdvancedEncryptionService _encryptionService;

    public Worker(ILogger<Worker> logger, ITcpClient tcpClient, IAdvancedEncryptionService encryptionService)
    {
        _logger = logger;
        _tcpClient = tcpClient;
        _encryptionService = encryptionService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                var payload = "Hello, World!"u8.ToArray();
                var encryptedPayload = _encryptionService.Encrypt(payload);
                var response = await _tcpClient.SendAsync(IPAddress.Parse("10.0.0.28"), 6667, encryptedPayload);
                var decryptedResponse = _encryptionService.Decrypt(response);
                
                if (decryptedResponse.Length > 0)
                {
                    _logger.LogInformation("Received: {Response}", Encoding.UTF8.GetString(decryptedResponse.ToArray()));
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}