using Microsoft.AspNetCore.Mvc;
using EgeApp.Backend.Shared.Dtos.ResponseDtos;

namespace EgeApp.Backend.Shared.Helpers
{
    public class CustomControllerBase : ControllerBase
    {
        public static IActionResult CreateActionResult<T>(ResponseDto<T> responseDto)
        {
            return new ObjectResult(responseDto)
            {
                StatusCode = responseDto.StatusCode
            };
        }
    }
}