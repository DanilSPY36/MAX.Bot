using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Types
{
    public sealed class UploadedFile : IDisposable
    {
        private UploadedFile(Stream stream, string fileName)
        {
            this.stream = stream;
            FileName = fileName;
            Lenght = stream.Length;
        }

        internal Stream stream { get; }
        public string FileName { get; }
        public Int64 Lenght { get; }

        [JsonPropertyName("file_id")]
        public string FileId { get; init; } = default!;

        [JsonPropertyName("size")]
        public Int64 Size { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; } = default!;

        public static UploadedFile FromPath(string path)
        {
            var stream = File.OpenRead(path);
            return new UploadedFile(stream, Path.GetFileName(path));
        }
        public static UploadedFile FromStream(Stream stream, string? fileName = null)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            if (!stream.CanRead)
                throw new ArgumentException("Stream must be readable");
            
            if (string.IsNullOrEmpty(fileName))
            {
                if (stream is FileStream fs)
                {
                    fileName = Path.GetFileName(fs.Name);
                }
                else
                {
                    throw new ArgumentException("File name must be provided when stream is not a FileStream", nameof(fileName));
                }
            }
            return new UploadedFile(stream, fileName);
        }

        public void Dispose()
        {
            stream.Dispose();
        }
    }
}
