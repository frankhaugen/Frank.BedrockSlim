namespace Frank.BedrockSlim.Cryptography;

public class AdvancedEncryptionService : IAdvancedEncryptionService
{
    private readonly IAdvancedEncryptionFactory _advancedEncryptionFactory;
    private readonly AdvancedEncryptionOptions _options;

    public AdvancedEncryptionService(IAdvancedEncryptionFactory advancedEncryptionFactory, AdvancedEncryptionOptions options)
    {
        _advancedEncryptionFactory = advancedEncryptionFactory;
        _options = options;
    }

    public ReadOnlyMemory<byte> Encrypt(ReadOnlyMemory<byte> data)
    {
        using var aes = _advancedEncryptionFactory.Create(_options.ToAesKey());
        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        return encryptor.TransformFinalBlock(data.ToArray(), 0, data.Length);
    }

    public ReadOnlyMemory<byte> Decrypt(ReadOnlyMemory<byte> data)
    {
        using var aes = _advancedEncryptionFactory.Create(_options.ToAesKey());
        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        return decryptor.TransformFinalBlock(data.ToArray(), 0, data.Length);
    }
}