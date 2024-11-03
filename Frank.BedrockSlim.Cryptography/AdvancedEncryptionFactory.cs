using System.Security.Cryptography;

namespace Frank.BedrockSlim.Cryptography;

public class AdvancedEncryptionFactory : IAdvancedEncryptionFactory
{
    private readonly Dictionary<AesKey, Aes> _aesCache = new();
    
    public Aes Create(AesKey aesKey)
    {
        if (_aesCache.TryGetValue(aesKey, out var aes))
        {
            return aes;
        }

        aes = Aes.Create();
        aes.Key = aesKey.Key;
        aes.IV = aesKey.Iv;
        _aesCache.Add(aesKey, aes);
        return aes;
    }
}