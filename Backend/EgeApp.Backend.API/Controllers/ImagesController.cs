using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EgeApp.Backend.Shared.Dtos.ImageDtos;
using EgeApp.Backend.Shared.Helpers;
using EgeApp.Backend.Shared.Helpers.Abstract;

namespace EgeApp.Backend.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImagesController : CustomControllerBase
    {
        private readonly IImageHelper _imageHelper;

        public ImagesController(IImageHelper imageHelper)
        {
            _imageHelper = imageHelper;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(ImageCreateDto imageCreateDto)
        {
            var response = await _imageHelper.UploadImageAsync(imageCreateDto);
            return CreateActionResult(response);
        }

        [HttpDelete]
        public IActionResult DeleteImage(ImageDeleteDto imageDeleteDto)
        {
            var response = _imageHelper.DeleteImage(imageDeleteDto);
            return CreateActionResult(response);
        }
    }
}
