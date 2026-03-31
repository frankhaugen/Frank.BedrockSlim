using Frank.BedrockSlim.Cryptography;

namespace Frank.BedrockSlim.Tests;

public class AdvancedEncryptionServiceTests
{
    [Fact]
    public void Encrypt_then_Decrypt_roundtrips_plaintext()
    {
        var options = new AdvancedEncryptionOptions
        {
            Key = "0123456789ABCDEF0123456789ABCDEF",
            Iv = "0123456789ABCDEF"
        };
        var factory = new AdvancedEncryptionFactory();
        var service = new AdvancedEncryptionService(factory, options);
        ReadOnlyMemory<byte> plain = "hello"u8.ToArray();

        var cipher = service.Encrypt(plain);
        Assert.NotEqual(plain.ToArray(), cipher.ToArray());

        var roundTrip = service.Decrypt(cipher);
        Assert.Equal(plain.ToArray(), roundTrip.ToArray());
    }
}
