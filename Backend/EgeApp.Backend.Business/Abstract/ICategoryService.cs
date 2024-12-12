using System.Linq.Expressions;
using EgeApp.Backend.Models;
using EgeApp.Backend.Shared.Dtos.CategoryDtos;
using EgeApp.Backend.Shared.Dtos.ResponseDtos;
using EgeApp.Backend.Shared.ResponseDtos;

namespace EgeApp.Backend.Business.Abstract
{
    public interface ICategoryService
    {
        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<ResponseDto<CategoryDto>> CreateWithSubCategoriesAsync(CategoryCreateDto categoryCreateDto);
        Task<ResponseDto<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<ResponseDto<NoContent>> DeleteAsync(int id);
        Task<ResponseDto<CategoryDto>> GetByIdAsync(int id);
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();
        Task<ResponseDto<List<CategoryDto>>> GetActivesAsync(bool isActive = true);
        Task<ResponseDto<int>> GetCountAsync();
        Task<ResponseDto<int>> GetActivesCountAsync(bool isActive = true);
        Task<ResponseDto<List<CategoryDto>>> GetHomeCategoriesAsync();
        Task<ResponseDto<(List<CategoryDto>, int)>> GetPagedCategoriesAsync(int pageIndex, int pageSize);
        Task<ResponseDto<List<CategoryDto>>> GetSortedCategoriesAsync<TKey>(Expression<Func<Category, TKey>> orderBy, bool isDescending = false);
        Task<ResponseDto<NoContent>> AddCategoriesAsync(IEnumerable<CategoryCreateDto> categories);
        Task<ResponseDto<NoContent>> UpdateCategoriesAsync(IEnumerable<CategoryUpdateDto> categories);
    }
}
