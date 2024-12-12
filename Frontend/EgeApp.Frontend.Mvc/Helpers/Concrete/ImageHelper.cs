using System;

namespace EgeApp.Frontend.Mvc.Helpers.Concrete;

public static class ImageHelper
{
    public static async Task<(bool IsSucceeded, string Data, string Error)> UploadImageAsync(IFormFile imageFile, string folderName)
    {
        if (imageFile == null || imageFile.Length <= 0)
            return (false, null, "Dosya geçersiz.");

        try
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

      
            var imageUrl = $"/{folderName}/{fileName}";
            return (true, imageUrl, null);
        }
        catch (Exception ex)
        {
            return (false, null, $"Dosya yüklenirken bir hata oluştu: {ex.Message}");
        }
    }
}
