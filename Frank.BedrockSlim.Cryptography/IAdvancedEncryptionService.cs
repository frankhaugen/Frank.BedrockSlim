namespace Frank.BedrockSlim.Cryptography;

public interface IAdvancedEncryptionService
{
    ReadOnlyMemory<byte> Encrypt(ReadOnlyMemory<byte> data);
    ReadOnlyMemory<byte> Decrypt(ReadOnlyMemory<byte> data);
}