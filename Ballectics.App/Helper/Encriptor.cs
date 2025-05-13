using System.Security.Cryptography;
using System.Text;

namespace Ballectics.App.Helper;

public static class Encriptor
{
    private static string KEY = "Mgh@9450052264601450012323@#$!@M";
    public static string Encrypt(string plainText)
    {
        var aes = Aes.Create();
        var keyBytes = Encoding.UTF8.GetBytes(KEY.PadRight(32))
                               .Take(32).ToArray();
        aes.Key = keyBytes;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        var result = Convert.ToBase64String(aes.IV.Concat(encryptedBytes).ToArray());
        return result;
    }
    public static string Decrypt(string encryptedText)
    {
        var fullBytes = Convert.FromBase64String(encryptedText);
        var iv = fullBytes.Take(16).ToArray();
        var cipherText = fullBytes.Skip(16).ToArray();

        var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(KEY.PadRight(32)).Take(32).ToArray();
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        var plainBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
        return Encoding.UTF8.GetString(plainBytes);
    }
}
