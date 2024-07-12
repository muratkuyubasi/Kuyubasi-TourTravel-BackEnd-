using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace TourV2.Helper
{
    public class FileUpload
    {
        public static string UploadDocument(FileUploadDTO fileModel)
        {
            try
            {
                var filePath = $"{fileModel.RootPath}/{fileModel.FolderName}";
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var mediaFile = fileModel.FormFile;
                var newMedia = $"{Guid.NewGuid()}{Path.GetExtension(mediaFile.FileName)}";
                string fullPath = Path.Combine(filePath, newMedia);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    mediaFile.CopyTo(stream);
                }
                return $"/{fileModel.FolderName}/{newMedia}";
            }
            catch (Exception ex)
            {
                return "Hata oluştu: " + ex.Message;
            }
        }
    }
    public class FileUploadDTO
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public string RootPath { get; set; }
        [Required]
        public string FolderName { get; set; }
        [Required]
        public IFormFile FormFile { get; set; }
    }
}
