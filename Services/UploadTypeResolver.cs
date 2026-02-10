using MAX.Bot.Models.Enums;
using MAX.Bot.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Services
{
    internal static class UploadTypeResolver
    {
        public static UploadType Resolve(UploadedFile file) => Resolve(file.FileName);
        public static UploadType Resolve(string fileName)
        {
            var extention = Path.GetExtension(fileName);
            extention = extention.ToLower();
            return extention switch
            {
                ".jpg" or ".jpeg" or ".png" or ".git" or ".webp" => UploadType.image,
                ".mp4" or ".mov" or ".avi" or ".mkv" => UploadType.video,
                ".mp3" or ".wav" or ".ogg" => UploadType.audio,
                _ => UploadType.file
            };
        }
    }
}
