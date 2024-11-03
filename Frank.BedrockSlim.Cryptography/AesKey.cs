namespace Frank.BedrockSlim.Cryptography;

public record struct AesKey(byte[] Key, byte[] Iv);