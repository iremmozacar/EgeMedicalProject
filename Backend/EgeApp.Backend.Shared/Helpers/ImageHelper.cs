using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using EgeApp.Backend.Shared.Dtos.ImageDtos;
using EgeApp.Backend.Shared.Dtos.ResponseDtos;
using EgeApp.Backend.Shared.Helpers.Abstract;

namespace EgeApp.Backend.Shared.Helpers
{
    public class ImageHelper : IImageHelper
    {
        private readonly string _imagesFolder;
        public ImageHelper(IWebHostEnvironment webHostEnvironment)
        {
            _imagesFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
        }

        private bool IsImageValid(string extension)
        {
            string[] correctExtensions = [".png", ".jpeg", ".jpg"];
            bool result = correctExtensions.Contains(extension);
            return result;
        }

        public async Task<ResponseDto<ImageDto>> UploadImageAsync(ImageCreateDto imageCreateDto)
        {

            if (imageCreateDto.Image == null || imageCreateDto.Image.Length == 0)
            {
                return ResponseDto<ImageDto>.Fail("Bir hata oluştu!", StatusCodes.Status400BadRequest);
            }
            var imageExtension = Path.GetExtension(imageCreateDto.Image.FileName);
            if (!IsImageValid(imageExtension))
            {
                return ResponseDto<ImageDto>.Fail($"Geçersiz format!({imageExtension})", StatusCodes.Status400BadRequest);
            }
           
            var targetFolder = Path.Combine(_imagesFolder, imageCreateDto.FolderName ?? "general");
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
            var fileName = $"{Guid.NewGuid()}{imageExtension}";
            var fullPath = Path.Combine(targetFolder, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            imageCreateDto.Image.CopyTo(stream);
            ImageDto imageDto = new()
            {
                
                Url = Path.Combine("http://localhost:5200", "images", imageCreateDto.FolderName ?? "general", fileName),
                Name = fileName
            };
            return ResponseDto<ImageDto>.Success(imageDto, StatusCodes.Status201Created);
        }

        public ResponseDto<NoContent> DeleteImage(ImageDeleteDto imageDeleteDto)
        {
            
            var fullPath = Path.Combine(_imagesFolder, imageDeleteDto.FolderName, imageDeleteDto.FileName);
            if (!File.Exists(fullPath))
            {
                return ResponseDto<NoContent>.Fail("Böyle bir resim bulunamadı!", StatusCodes.Status404NotFound);
            }
            File.Delete(fullPath);
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }
    }
}
