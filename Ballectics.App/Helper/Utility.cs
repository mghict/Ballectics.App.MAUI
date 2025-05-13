using System.Text;
using QRCoder;

namespace Ballectics.App.Helper;

public static class Utility
{
    public static ImageSource GetImageSource(this byte[] imageBytes)
    {
        if (imageBytes == null || imageBytes.Length == 0)
            return null;

        return ImageSource.FromStream(() => new MemoryStream(imageBytes));
    }

    public static async Task<byte[]> ConvertStreamToByteArray(this Stream inputStream)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            await inputStream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public static ImageSource GenerateQrCode(string text)
    {
        //QRCoder
        text = ToBase64(text);

        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qrCode.GetGraphic(20);

        return ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
    }

    public static byte[] GenerateQrCodeByte(string text)
    {
        //text = ToBase64(text);

        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qrCode.GetGraphic(20);

        return qrCodeBytes;
    }

    public static string ToBase64(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(bytes);
    }
}
