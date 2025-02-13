using CodeUp.Common.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Modules.Students.Application.Helpers;

public static class Base64FileConverter
{
    public static Response<IFormFile> ConvertBase64ToIFormFile(string base64String, string? fileName = null)
    {
        int commaIndex = base64String.IndexOf(",");
        string mimeType = "application/octet-stream";
        if (commaIndex > 0)
        {
            string prefix = base64String[..commaIndex];
            if (prefix.StartsWith("data:"))
            {
                mimeType = prefix[5..prefix.IndexOf(";")];
            }
            base64String = base64String[(commaIndex + 1)..];
        }

        byte[]? imageBytes;

        try
        {
            imageBytes = Convert.FromBase64String(base64String);
        }
        catch
        {
            return new Response<IFormFile>(null, 400, "Your image is in invalid format");
        }

        string extension = GetFileExtensionFromMimeType(mimeType);

        if (string.IsNullOrWhiteSpace(extension))
            return new(null, 400, "Your image is in invalid format");

        fileName ??= $"image-{Guid.NewGuid()}{extension}";
        var stream = new MemoryStream(imageBytes);

        var file = new FormFile(stream, 0, stream.Length, "file", fileName)
        {
            Headers = new HeaderDictionary
                {
                    { "Content-Type", mimeType }
                },
            ContentType = mimeType
        };

        return new(file, 200);
    }

    private static string GetFileExtensionFromMimeType(string mimeType) =>
        mimeType.ToLower() switch
        {
            "image/jpeg" => ".jpg",
            "image/png" => ".png",
            "image/gif" => ".gif",
            "image/bmp" => ".bmp",
            "image/webp" => ".webp",
            "image/svg+xml" => ".svg",
            "application/pdf" => ".pdf",
            "text/plain" => ".txt",
            "application/msword" => ".doc",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => ".docx",
            _ => string.Empty
        };
}
