namespace LiteStreaming.Cryptography;
public interface ICrypto
{
    string Encrypt(string password);
    string Decrypt(string encryptedText);
}