using System.Security.Cryptography;

namespace Frank.BedrockSlim.Cryptography;

public interface IAdvancedEncryptionFactory
{
    Aes Create(AesKey key);
}