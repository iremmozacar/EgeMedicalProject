using System;

namespace EgeApp.Frontend.Mvc.Extensions;
public static class StreamExtensions
{
    public static byte[] ToByteArray(this Stream stream)
    {
        if (!stream.CanRead)
        {
            throw new InvalidOperationException("Stream cannot be read.");
        }

        if (stream.CanSeek)
        {
            Console.WriteLine($"Resetting stream position. Length: {stream.Length}");
            stream.Seek(0, SeekOrigin.Begin);
        }

        byte[] buffer = new byte[16 * 1024]; 
        using (var ms = new MemoryStream())
        {
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                Console.WriteLine($"Bytes Read: {read}");
                ms.Write(buffer, 0, read);
            }

            if (ms.Length == 0)
            {
                throw new InvalidOperationException("Stream did not contain any data.");
            }

            Console.WriteLine($"Total Bytes Written: {ms.Length}");
            return ms.ToArray();
        }
    }
}
