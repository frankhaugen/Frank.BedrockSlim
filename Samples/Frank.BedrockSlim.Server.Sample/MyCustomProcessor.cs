using System.Text;
using Frank.BedrockSlim.Cryptography;

namespace Frank.BedrockSlim.Server.Sample;

public class MyCustomProcessor : IConnectionHandler
{
    private readonly ILogger<MyCustomProcessor> _logger;
    private readonly IAdvancedEncryptionService _encryptionService;

    public MyCustomProcessor(ILogger<MyCustomProcessor> logger, IAdvancedEncryptionService encryptionService)
    {
        _logger = logger;
        _encryptionService = encryptionService;
    }

    /// <inheritdoc />
    public async Task<ReadOnlyMemory<byte>> HandleAsync(ReadOnlyMemory<byte> input)
    {
        var decryptedInput = _encryptionService.Decrypt(input);
        var stringInput = Encoding.UTF8.GetString(decryptedInput.ToArray());
        _logger.LogInformation("Received: {Input}", stringInput);
        return await Task.FromResult(_encryptionService.Encrypt(Encoding.UTF8.GetBytes($"Echo: {stringInput}")));
    }
}