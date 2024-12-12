using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using EgeApp.Backend.Shared.Dtos.ImageDtos;
using EgeApp.Backend.Shared.Dtos.ResponseDtos;

namespace EgeApp.Backend.Shared.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<ResponseDto<ImageDto>> UploadImageAsync(ImageCreateDto imageCreateDto);
        ResponseDto<NoContent> DeleteImage(ImageDeleteDto imageDeleteDto);
    }
}
