using System.Text;

namespace Frank.BedrockSlim.Cryptography;

public class AdvancedEncryptionOptions
{
    public string Key { get; set; } = "puDUtQJOf5UBY0iI0PwKStlBeHBEn123"; // 32 bytes, 256 bits, Should be changed
    public string Iv { get; set; } = "0123456789ABCDEF"; // 16 bytes, 128 bits, Should be changed
    
    public AesKey ToAesKey() => new(Encoding.UTF8.GetBytes(Key), Encoding.UTF8.GetBytes(Iv));
}